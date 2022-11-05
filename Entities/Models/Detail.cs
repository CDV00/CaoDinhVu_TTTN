using System;

namespace Entities.Models
{
    public class Detail : BaseEntity<Guid>
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
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

    }
}
