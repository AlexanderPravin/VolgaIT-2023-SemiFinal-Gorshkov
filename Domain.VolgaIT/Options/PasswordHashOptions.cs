using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VolgaIT.Options
{
    public class PasswordHashOptions
    {
        public const string JsonSection = "PasswordHashOptions";

        public int KeySize { get; set; } = 8;

        public int SaltSize { get; set; } = 16;

        public int Iterations { get; set; } = 1000;
    }
}
