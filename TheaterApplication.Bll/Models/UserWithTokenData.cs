using System;
using System.Collections.Generic;
using System.Text;

namespace TheaterApplication.Bll.Models
{
    public class UserWithTokenData: User
    {
        public DateTime? Expired { get; set; }
    }
}
