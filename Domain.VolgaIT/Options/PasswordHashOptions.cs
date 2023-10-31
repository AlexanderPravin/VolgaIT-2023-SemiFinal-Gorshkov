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

        public int KeySize {  get; set; }

        public int SaltSize { get; set; }

        public int Iterations { get; set; } 
    }
}
