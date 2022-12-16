using System;

namespace Entities.Requests
{
    public class SliderRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = "Slider";
        public string Img { get; set; }
        public int? Orders { get; set; } = 10;
        public int? Status { get; set; } = 1;
    }
}
