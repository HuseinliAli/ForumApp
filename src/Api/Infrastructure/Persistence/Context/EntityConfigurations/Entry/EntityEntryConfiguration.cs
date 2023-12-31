using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context.EntityConfigurations.Entry;

public class EntityEntryConfiguration : BaseEntityConfiguration<Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.Entry> builder)
    {
        base.Configure(builder);
        builder.ToTable("entries", ForumAppContext.DEFAULT_SHCEMA);
        builder.HasOne(x => x.User).WithMany(x => x.Entries).HasForeignKey(x => x.UserId);
    }
}
