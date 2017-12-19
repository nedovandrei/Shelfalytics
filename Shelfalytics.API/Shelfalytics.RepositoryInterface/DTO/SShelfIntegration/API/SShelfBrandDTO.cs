using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.SShelfIntegration.API
{
    public class SShelfBrandDTO
    {
        /// <summary>
        /// Brand id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product code in vendor's system
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Brand Family (SO WHY IT'S CALLED "CAT" THEN?! UUGH)
        /// </summary>
        public string Cat { get; set; }

        /// <summary>
        /// Brand itself (yeah right, subcategory my ass)
        /// </summary>
        public string Sub_cat { get; set; }

        /// <summary>
        /// Name of the product (what's the fuckin difference with the previous field?)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Status, 0 - inactive, 1 - active (why the fuck wouldn't you use BOOLEAN goddammit)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Price (WHY?! WHY DID YOU NOT CALL ALL YOUR FIELDS THIS WAY! SELF-EXPLA-NA-TO-RY)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Trade equipment type Id
        /// </summary>
        public int Id_type { get; set; }

        /// <summary>
        /// Format (WHAT THE FUCK DOES THAT SUPPOSE TO MEAN)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Pack width (goddammit)
        /// </summary>
        public int Width { get; set; }
    }
}
