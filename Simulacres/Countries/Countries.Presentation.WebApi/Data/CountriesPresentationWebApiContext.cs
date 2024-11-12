using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Countries.Infrastructure.Contracts.Entities;

namespace Countries.Presentation.WebApi.Data
{
    public class CountriesPresentationWebApiContext : DbContext
    {
        public CountriesPresentationWebApiContext (DbContextOptions<CountriesPresentationWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Countries.Infrastructure.Contracts.Entities.Country> Country { get; set; } = default!;
    }
}
