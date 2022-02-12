using Course.Services.Catalog.Dtos.CategoryDto;
using Course.Services.Catalog.Dtos.CourseDto;
using Course.Services.Catalog.Services.IService;
using Course.Shared.ControllerBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseService.GetAllAsync();
            return CreateActionResultInstance(result);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(result);
        }

        [HttpGet]
        [Route("GetAllByUserId")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var result = await _courseService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var result = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(result);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(CategoryUpdateDtos categoryUpdateDtos)
        {
            var result = await _courseService.UpdateAsync(categoryUpdateDtos);
            return CreateActionResultInstance(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(result);
        }
    }
}
