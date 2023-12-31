using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.EntityConfigurations.Entry;

public class EntityEntryFavoriteConfiguration : BaseEntityConfiguration<Domain.Models.EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.EntryFavorite> builder)
    {
        base.Configure(builder);
        builder.ToTable("entry_favorites", ForumAppContext.DEFAULT_SHCEMA);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryFavorites)
            .HasForeignKey(x => x.EntryId);

        builder.HasOne(x => x.User)
          .WithMany(x => x.EntryFavorites)
          .HasForeignKey(x => x.UserId);
    }
}
