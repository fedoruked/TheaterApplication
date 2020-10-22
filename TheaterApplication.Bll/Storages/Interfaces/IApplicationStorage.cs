using TheaterApplication.Bll.Models;

namespace TheaterApplication.Bll.Storages.Interfaces
{
    public interface IApplicationStorage
    {
        UserWithTokenData User { get; set; }
    }
}
