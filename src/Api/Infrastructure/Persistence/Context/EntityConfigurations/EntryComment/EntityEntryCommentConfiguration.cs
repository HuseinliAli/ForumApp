using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context.EntityConfigurations.EntryComment;

public class EntityEntryCommentConfiguration : BaseEntityConfiguration<Domain.Models.EntryComment>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.EntryComment> builder)
    {
        base.Configure(builder);
        builder.ToTable("entry_comments",ForumAppContext.DEFAULT_SHCEMA);
        builder.HasOne(x => x.User)
            .WithMany(x => x.EntryComments)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryComments)
            .HasForeignKey(x => x.EntryId);
    }
}