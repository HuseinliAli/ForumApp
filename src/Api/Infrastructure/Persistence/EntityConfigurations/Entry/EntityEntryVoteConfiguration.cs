using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.EntityConfigurations.Entry;

public class EntityEntryVoteConfiguration : BaseEntityConfiguration<Domain.Models.EntryVote>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.EntryVote> builder)
    {
        base.Configure(builder);
        builder.ToTable("entry_votes", ForumAppContext.DEFAULT_SHCEMA);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryVotes)
            .HasForeignKey(x => x.EntryId);
    }
}
