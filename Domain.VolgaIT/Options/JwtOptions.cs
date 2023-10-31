using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VolgaIT.Settings
{
    public class JwtOptions
    {
        public const string JsonSection = "JwtOptions";

        public string Issuer { get; set; } = "TestIssuer";

        public string Secret { get;set; } = "asdfghjkl123456789qwe";

        public int ExpiresHours { get; set; } = 2;
    }
}
