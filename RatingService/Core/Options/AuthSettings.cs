using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Options
{
    public interface IAuthSettings 
    { 
        string Secret { get; set; }
    }

    public class AuthSettings : IAuthSettings
    {
        public string Secret { get; set; }
    }
}
