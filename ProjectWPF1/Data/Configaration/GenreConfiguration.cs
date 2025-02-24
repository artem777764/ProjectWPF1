using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace ProjectWPF1.Data.Configaration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasMany(g => g.Books)
               .WithOne(b => b.Genre)
               .OnDelete(DeleteBehavior.Cascade);
    }
}