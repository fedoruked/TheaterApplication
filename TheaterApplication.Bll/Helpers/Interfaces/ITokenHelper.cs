using TheaterApplication.Bll.Models;

namespace TheaterApplication.Bll.Helpers.Interfaces
{
    public interface ITokenHelper
    {
        UserWithTokenData DecryptData(string encriptedJson);
        string EncyptData(UserWithTokenData userWithTokenData);
    }
}
