using Microsoft.EntityFrameworkCore;
using Npgsql;
using TeacherTracker.Api.Data;
using TeacherTracker.Api.DTOs.Schools;
using TeacherTracker.Api.Models;

namespace TeacherTracker.Api.Services
{
    public class SchoolService
    {
        private readonly ApplicationDbContext _context;
        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SchoolResponse>> GetAllAsync()
        {
            return await _context.Schools
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new SchoolResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    OIB = s.OIB,
                    Address = s.Address,
                    City = s.City,
                    IsActive = s.IsActive
                })
                .ToListAsync();
        }

        public async Task<SchoolResponse?> GetByIdAsync(int id)
        {
            return await _context.Schools
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new SchoolResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    OIB = s.OIB,
                    Address = s.Address,
                    City = s.City,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<SchoolResponse?> CreateAsync(CreateSchoolRequest request)
        {
            bool exists = await _context.Schools
                .AnyAsync(s => s.OIB == request.OIB);

            if (exists) return null;

            var school = new School
            {
                Name = request.Name,
                OIB = request.OIB,
                Address = request.Address,
                City = request.City,
                IsActive = true
            };

            _context.Schools.Add(school);

            await _context.SaveChangesAsync();

            return new SchoolResponse
            {
                Id = school.Id,
                Name = school.Name,
                OIB = school.OIB,
                Address = school.Address,
                City = school.City,
                IsActive = school.IsActive
            };
        }

        public async Task<SchoolResponse?> UpdateAsync(int id, UpdateSchoolRequest request)
        {
            var school = await _context.Schools
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school is null) return null;

            school.Name = request.Name;
            school.OIB = request.OIB;
            school.Address = request.Address;
            school.City = request.City;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException pg &&
                      pg.SqlState == PostgresErrorCodes.UniqueViolation &&
                      pg.ConstraintName == "ix_schools_oib")
            {
                return null;
            }

            return new SchoolResponse
            {
                Id = school.Id,
                Name = school.Name,
                OIB = school.OIB,
                Address = school.Address,
                City = school.City,
                IsActive = school.IsActive
            };
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var school = await _context.Schools
                .FirstOrDefaultAsync (s => s.Id == id);

            if (school is null) return false;

            if(!school.IsActive) return true;

            school.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        } 
    }
}
