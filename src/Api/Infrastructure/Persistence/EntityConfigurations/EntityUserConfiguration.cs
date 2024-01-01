using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.EntityConfigurations;

public class EntityUserConfiguration : BaseEntityConfiguration<Domain.Models.User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("users", ForumAppContext.DEFAULT_SHCEMA);
    }
}
