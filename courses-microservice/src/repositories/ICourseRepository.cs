using course_microservice.context;
using course_microservice.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace course_microservice.repositories
{
    public interface ICourseRepository
    {
        Task<List<CourseModel>> GetAllCourses();
        Task<CourseModel> GetCourse(int ID);
        Task<CourseModel> AddCourse(CourseModel course);
        Task<CourseModel> UpdateCourse(int ID, CourseModel updatedCourse);
        Task<bool> DeleteCourse(int ID);
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly MyDbContext _dbContext;

        public CourseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CourseModel>> GetAllCourses()
        {
            return await _dbContext.Course.ToListAsync();
        }

        public async Task<CourseModel> GetCourse(int ID)
        {
            return await _dbContext.Course.FindAsync(ID);
        }

        public async Task<CourseModel> AddCourse(CourseModel course)
        {
            // Verificar si ya existe un curso con el mismo ID
            var existingCourse = await _dbContext.Course.FindAsync(course.ID);
            if (existingCourse != null)
            {
                // Si ya existe un curso con el mismo ID, no lo agregamos y devolvemos null
                return null;
            }

            // Si no hay un curso con el mismo ID, lo agregamos a la base de datos
            _dbContext.Course.Add(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<CourseModel> UpdateCourse(int ID, CourseModel updatedCourse)
        {
            var course = await _dbContext.Course.FindAsync(ID);
            if (course != null)
            {
                course.Name = updatedCourse.Name;
                course.Semester = updatedCourse.Semester;
                course.Credits = updatedCourse.Credits;
                await _dbContext.SaveChangesAsync();
            }
            return course;
        }

        public async Task<bool> DeleteCourse(int ID)
        {
            var course = await _dbContext.Course.FindAsync(ID);
            if (course != null)
            {
                _dbContext.Course.Remove(course);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
