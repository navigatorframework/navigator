using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Abstractions;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Samples.Echo.Entity
{
    public class SampleUser : User
    {
        public string Secret { get; set; }
    }

    public class SampleUserEntityTypeConfiguration : IEntityTypeConfiguration<SampleUser>
    {
        public void Configure(EntityTypeBuilder<SampleUser> builder)
        {
            builder.Property(e => e.Secret)
                .HasMaxLength(100);
        }
    }
    
    public class SampleUserMapper : IUserMapper<SampleUser>
    {
        public SampleUser Parse(Telegram.Bot.Types.User user)
        {
            return new SampleUser
            {
                Id = user.Id,
                IsBot = user.IsBot,
                LanguageCode = user.LanguageCode,
                Username = user.Username,
                Secret = "no secret"
            };
        }
    }
}