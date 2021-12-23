using Navigator.Entities;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Mappers;

public interface IProviderProfileMapper<TProfile>
{
    TProfile? From(Conversation conversation);

    UniversalProfile? To(TProfile profile);
}