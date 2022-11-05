using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            //valudation
            builder.HasQueryFilter(u => !u.IsDelete);
            //
            //builder.HasKey(x => x.Id);
            builder.HasIndex(u => u.Orders).IsUnique();
        }
    }
}
