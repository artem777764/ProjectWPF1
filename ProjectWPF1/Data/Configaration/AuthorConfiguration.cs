using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace ProjectWPF1.Data.Configaration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasMany(a => a.Books)
               .WithOne(b => b.Author)
               .OnDelete(DeleteBehavior.Cascade);
    }
}