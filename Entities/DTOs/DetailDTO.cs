using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class DetailDTO:BaseDTO
    {
        public string Screen { get; set; }
        public string Camera { get; set; }
        public string OperatingSystem { get; set; }
        public string CPU { get; set; }
        public string ROM { get; set; }
        public string RAM { get; set; }
        public string Connection { get; set; }
        public string Battery { get; set; }
        public string Charger { get; set; }
        public string GeneralInformation { get; set; }
    }
}
