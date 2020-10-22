namespace TheaterApplication.Bll.Models.Enums
{
    public enum AuthorizationResultEnum: byte
    {
        OK = 1,
        UserNotFound,
        TokenExpired,
        RoleNotAllowed
    }
}
