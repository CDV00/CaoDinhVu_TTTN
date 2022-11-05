using System;

namespace Entities.Requests
{
    public class CategoryRequest
    {
        public Guid? Id { get; set; }

        //[Required(ErrorMessage = "Bắt buộc nhập tên danh mục")]
        public string Name { get; set; }
        public string? Slug { get; set; }
        public Guid? ParentId { get; set; }
        public int? Orders { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }
    }
}
