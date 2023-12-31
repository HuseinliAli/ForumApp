using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.EntityConfigurations;

public class EntityEmailConfirmationConfiguration : BaseEntityConfiguration<Domain.Models.EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);
        builder.ToTable("email_confirmations",ForumAppContext.DEFAULT_SHCEMA);
    }
}
