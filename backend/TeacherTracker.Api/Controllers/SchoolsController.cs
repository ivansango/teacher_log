using Microsoft.AspNetCore.Mvc;
using TeacherTracker.Api.Services;
using TeacherTracker.Api.DTOs.Schools;

namespace TeacherTracker.Api.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolService _schoolService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SchoolsController(SchoolService schoolService, IWebHostEnvironment webHostEnvironment)
        {
            _schoolService = schoolService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<SchoolResponse>>> GetAllAsync()
        {
            if(!_webHostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var schools = await _schoolService.GetAllAsync();
            return Ok(schools);
        }

        [HttpGet("{id:int}", Name = "GetSchoolById")]
        public async Task<ActionResult<SchoolResponse>> GetByIdAsync(int id)
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var school = await _schoolService.GetByIdAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        [HttpPost]
        public async Task<ActionResult<SchoolResponse>> Create([FromBody] CreateSchoolRequest request)
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var school = await _schoolService.CreateAsync(request);

            if(school == null)
            {
                return Conflict(new
                {
                    message = "Škola s tim OIB-om već postoji!"
                });
            }

            return CreatedAtRoute(
                "GetSchoolById",
                new { id = school.Id },
                school);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SchoolResponse>> Update(int id, [FromBody] UpdateSchoolRequest request)
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var updated = await _schoolService.UpdateAsync(id, request);

            if (updated is null)
            {
                return NotFound(new
                {
                    message = "Škola s tim ID-om ne postoji ili krivi OIB."
                });
            }
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var deactivated = await _schoolService.DeactivateAsync(id);

            if(!deactivated) return NotFound();

            return Ok();
        }
    }
}
