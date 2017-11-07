using Shelfalytics.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.RepositoryInterface.DTO.Export;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml;

namespace Shelfalytics.Service
{
    public class ExportService : IExportService
    {
        private readonly IPointOfSaleRepository _pointOfSaleRepository;
        private readonly IEquipmentDataRepository _equipmentDataRepository;
        private readonly IProductDataRepository _productDataRepository;
        private readonly IExportRepository _exportRepository;
        private readonly IStatisticsService _statisticsService;

        public ExportService(IPointOfSaleRepository pointOfSaleRepository, IEquipmentDataRepository equipmentDataRepository, IProductDataRepository productDataRepository, IExportRepository exportRepository, IStatisticsService statisticsService)
        {
            if (pointOfSaleRepository == null)
            {
                throw new ArgumentNullException(nameof(pointOfSaleRepository));
            }

            if (equipmentDataRepository == null)
            {
                throw new ArgumentNullException(nameof(equipmentDataRepository));
            }

            if (productDataRepository == null)
            {
                throw new ArgumentNullException(nameof(productDataRepository));
            }

            if (exportRepository == null)
            {
                throw new ArgumentNullException(nameof(exportRepository));
            }

            if (statisticsService == null)
            {
                throw new ArgumentNullException(nameof(statisticsService));
            }

            _pointOfSaleRepository = pointOfSaleRepository;
            _equipmentDataRepository = equipmentDataRepository;
            _productDataRepository = productDataRepository;
            _exportRepository = exportRepository;
            _statisticsService = statisticsService;
        }

        public async Task<ExportSelects> GetFilterSelectsData(GlobalFilter filter)
        {
            var result = new ExportSelects()
            {
                Locals = new List<string>()
            };

            var poses = await _pointOfSaleRepository.GetPointsOfSales(filter.ClientId, filter.IsAdmin);
            var products = await _productDataRepository.GetClientsProducts(filter.ClientId, filter.IsAdmin);
            var equipments = await _equipmentDataRepository.GetEquipments(filter);

            result.PointsOfSale = poses;
            result.Products = products;
            result.Equipments = equipments;
            result.Locals = poses.Select(x => x.City).Distinct().ToList();

            return result;
        }

        public async Task<byte[]> GenerateExcelReport(ExportFilter filter)
        {
            var resultProductList = new List<ProductExportDTO>();

            //getting the list of client's products
            var clientProducts = await _productDataRepository.GetClientsProducts(filter);

            var salesSummary = await _statisticsService.GetProductSalesSummary(filter);

            var oosInMoneyAndUnitsList = await _statisticsService.GetLossesDueToOOSSummary(filter);

            var productOpens = await _exportRepository.GetProductOpenCount(filter);

            var productsDataExport = clientProducts.Select(x => new ProductExportDTO
            {
                Id = x.Id,
                SKUName = x.SKUName,
                ShortSKUName = x.ShortSKUName
            });

            //filling sales data
            foreach(var sale in salesSummary.Products)
            {
                if (productsDataExport.Any(x => x.Id == sale.ProductId))
                {
                    productsDataExport = productsDataExport.Select(x => 
                    {
                        if(x.Id == sale.ProductId)
                        {
                            x.Sales = sale.Sales;
                        }
                        return x;
                    }).ToList();
                }
            }

            //filling oos data
            foreach(var oosInMoneyAndUnits in oosInMoneyAndUnitsList.LossesByProducts)
            {
                if (productsDataExport.Any(x => x.Id == oosInMoneyAndUnits.ProductId))
                {
                    productsDataExport = productsDataExport.Select(x =>
                    {
                        if (x.Id == oosInMoneyAndUnits.ProductId)
                        {
                            x.OOSInMoney = Math.Round(oosInMoneyAndUnits.Losses, 2);
                            x.OOSInUnits = oosInMoneyAndUnits.AverageSalesPerOos;
                        }
                        return x;
                    }).ToList();
                }
            }

            //filling door open data
            foreach(var open in productOpens)
            {
                if (productsDataExport.Any(x => x.Id == open.ProductId))
                {
                    productsDataExport = productsDataExport.Select(x =>
                    {
                        if (x.Id == open.ProductId)
                        {
                            x.DoorOpens = open.OpenCount;
                            x.SalesByOpen = Math.Round((double)x.Sales / (double)open.OpenCount, 2);
                        }
                        return x;
                    }).ToList();
                }
            }

            var stream = new MemoryStream();
            var document = SpreadsheetDocument
                .Create(stream, SpreadsheetDocumentType.Workbook);

            var workbookpart = document.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();

            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheets = document.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            var sheet = new Sheet()
            {
                Id = document.WorkbookPart
                .GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = filter.Locale == "en" ? "Product Data" : filter.Locale ==  "ru" ? "Данные о Продуктах" : "Product Data"
            };
            sheets.AppendChild(sheet);

            UInt32 rowIndex = 0;
            var row = new Row { RowIndex = ++rowIndex };
            sheetData.AppendChild(row);
            var cellIndex = 0;

            var headerFields = filter.Locale == "en" ? new string[] { "SKU Name", "OOS In Units", "Losses Due to OOS", "Sales", "Parent Equipment Door Opens", "Sales/Door Opening" } :
                                filter.Locale == "ru" ? new string[] { "Имя Товара", "OOS в штуках", "Потери вследствие OOS", "Продажи", "Открытия дверей оборудования", "Продажи/Открытия дверей" } :
                                new string[] { "SKU Name", "OOS In Units", "Losses Due to OOS", "Sales", "Parent Equipment Door Opens", "Sales/Door Opening" };

            foreach (var header in headerFields)
            {
                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, header));
            }

            foreach (var rowData in productsDataExport)
            {
                cellIndex = 0;
                row = new Row { RowIndex = ++rowIndex };
                sheetData.AppendChild(row);

                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, rowData.SKUName ?? string.Empty));
                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, rowData.OOSInUnits.ToString() ?? string.Empty));
                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, rowData.OOSInMoney.ToString() ?? string.Empty));
                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, rowData.Sales.ToString() ?? string.Empty));
                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, rowData.DoorOpens.ToString() ?? string.Empty));
                row.AppendChild(CreateTextCell(ColumnLetter(cellIndex++), rowIndex, rowData.SalesByOpen.ToString() ?? string.Empty));

            }
            
            workbookpart.Workbook.Save();
            document.Close();

            return stream.ToArray();
        }
        private string ColumnLetter(int intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64)
                ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64)
                ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter,
                thirdLetter).Trim();
        }

        private Cell CreateTextCell(string header, UInt32 index,
            string text)
        {
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index
            };

            var istring = new InlineString();
            var t = new Text { Text = text };
            istring.AppendChild(t);
            cell.AppendChild(istring);
            return cell;
        }
    }
}
