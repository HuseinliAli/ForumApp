using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.EntityConfigurations.EntryComment;

public class EntityEntryCommentVoteConfiguration : BaseEntityConfiguration<Domain.Models.EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.EntryCommentVote> builder)
    {
        base.Configure(builder);
        builder.ToTable("entry_comment_votes", ForumAppContext.DEFAULT_SHCEMA);
        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentVotes)
            .HasForeignKey(x => x.EntryCommentId);
    }
}

