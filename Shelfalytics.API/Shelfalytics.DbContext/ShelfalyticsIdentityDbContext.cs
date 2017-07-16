using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;

namespace Shelfalytics.DbContext
{
    public class ShelfalyticsIdentityDbContext: IdentityDbContext<User>, IIdentityUnitOfWork
    {
        public ShelfalyticsIdentityDbContext() : base("ShelfalyticsDevDB") { }
    }
}
