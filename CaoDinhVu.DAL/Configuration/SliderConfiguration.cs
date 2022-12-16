using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaoDinhVu.DAL.Configuration
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            //valudation
            builder.HasQueryFilter(u => !u.IsDelete.Value);
            //
            //builder.HasKey(x => x.Id);
            //builder.HasIndex(u => u.Orders).IsUnique();
        }
    }


}
