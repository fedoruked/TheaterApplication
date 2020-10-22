using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Storages.Interfaces;

namespace TheaterApplication.Bll.Storages
{
    public class ApplicationStorage: IApplicationStorage
    {
        public UserWithTokenData User { get; set; }
    }
}
