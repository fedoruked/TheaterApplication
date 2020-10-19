using System;
using System.Collections.Generic;
using System.Text;

namespace TheaterApplication.Utils.Settings
{
    public class TokenSettings
    {
        public int LifeTimeMinutes { get; set; }
        public string EncriptionPassword { get; set; }
        public byte[] EncriptionSalt { get; set; }
    }
}
