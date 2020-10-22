namespace TheaterApplication.WebApi.ViewModels
{
    public class UserVm
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}
