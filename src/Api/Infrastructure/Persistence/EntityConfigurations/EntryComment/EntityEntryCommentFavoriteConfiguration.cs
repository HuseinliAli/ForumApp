using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.EntityConfigurations.EntryComment;

public class EntityEntryCommentFavoriteConfiguration : BaseEntityConfiguration<Domain.Models.EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.EntryCommentFavorite> builder)
    {
        base.Configure(builder);
        builder.ToTable("entry_comment_favorites", ForumAppContext.DEFAULT_SHCEMA);
        builder.HasOne(x => x.User)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict); ;
        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.EntryCommentId);
    }
}

