﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface ISaleRepository
    {
        Task RegisterSale(Sale sale);
    }
}
