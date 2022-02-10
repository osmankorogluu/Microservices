using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using Course.Services.Catalog.Dtos.CategoryDto;
using Course.Services.Catalog.Services.IService;


namespace Course.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {

        private readonly IMongoCollection<Course.Services.Catalog.Models.Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course.Services.Catalog.Models.Course>(databaseSettings.CategoryCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var result = await _courseCollection.Find(course => true).ToListAsync();
            if (result.Any())
            {
                foreach (var course in result)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.Id).FirstAsync();
                }
            }
            else
            {
                result = new List<Course.Services.Catalog.Models.Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(result), 200);
        }
        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var result = await _courseCollection.Find<Course.Services.Catalog.Models.Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }

            result.Category = await _categoryCollection.Find<Category>(x => x.Id == result.Id).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(result), 200);
        }

        public async Task<Response<CourseDto>> GetAllByUserIdAsync(string userId)
        {
            var result = await _courseCollection.Find<Course.Services.Catalog.Models.Course>(x => x.Id == userId).ToListAsync();
            if (result.Any())
            {
                foreach (var course in result)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }
            else
            {
                result = new List<Course.Services.Catalog.Models.Course>();
            }


            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(result), 200);

        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var result = _mapper.Map<Course.Services.Catalog.Models.Course>(courseCreateDto);
            result.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(result);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(result), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(Course.Services.Catalog.Dtos.CourseDto.CategoryUpdateDtos courseUpdateDto)
        {
            var course = _mapper.Map<Course.Services.Catalog.Models.Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.CourseId, course);
            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }

            return Response<NoContent>.Success(204);
        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course Not Found.", 404);
            }
        }

    }
}
