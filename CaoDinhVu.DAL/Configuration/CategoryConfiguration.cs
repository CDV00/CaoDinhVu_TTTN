using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaoDinhVu.DAL.Configuration
{
    public class CategoryConfiguration :IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //valudation
            builder.HasQueryFilter(u => !u.IsDelete);
            //
            //builder.HasKey(x => x.Id);
            builder.HasIndex(u => u.Orders).IsUnique();
        }
    }
}
