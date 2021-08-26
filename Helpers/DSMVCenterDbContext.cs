using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Helpers
{
    public class DSMVCenterDbContext : DbContext
    {
        private string _connectionString;

        public DSMVCenterDbContext([NotNull] DbContextOptions options) : base(options) { }

        public DSMVCenterDbContext(string connectionString)
        {
            this._connectionString = connectionString;
            this.Database.SetCommandTimeout(TimeSpan.FromMinutes(1));
        }

    }
}
