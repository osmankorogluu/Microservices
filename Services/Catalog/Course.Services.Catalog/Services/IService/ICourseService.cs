using Course.Services.Catalog.Dtos.CategoryDto;
using Course.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Services.IService
{
   public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<CourseDto>> GetAllByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(Course.Services.Catalog.Dtos.CourseDto.CategoryUpdateDtos courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
