using AutoMapper;
using Course.Services.Catalog.Dtos.CategoryDto;
using Course.Services.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Course.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course.Services.Catalog.Models.Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
