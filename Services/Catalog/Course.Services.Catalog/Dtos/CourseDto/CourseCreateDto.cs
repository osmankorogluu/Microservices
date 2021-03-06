using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Dtos.CategoryDto
{
    public class CourseCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public Course.Services.Catalog.Dtos.FeatureDto.FeatureDto FeatureDto { get; set; }
        public string CategoryId { get; set; }
    }
}
