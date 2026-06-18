using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Persistence;
using System;

namespace Dsw2026Ej15.Api.Controllers
{    public class CreateDoctorRequest
    {        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public Guid SpecialityId { get; set; }
    }

    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {   private readonly IPersistence _persistence;
                public DoctorsController(IPersistence persistence)
        {       _persistence = persistence;
        }
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("El nombre es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("La licencia es requerida.");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
                throw new ValidationException("La especialidad debe existir.");
               var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                name = request.Name,
                LicenseNumber = request.LicenseNumber,
                Speciality = speciality,
                IsActive = true
            };
            _persistence.addDoctor(newDoctor);
            return StatusCode(201, newDoctor);
        }
        [HttpGet]
        public IActionResult GetActiveDoctors()
        {      return Ok(_persistence.GetActiveDoctors());
        }
        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetActiveDoctorById(id);
            if (doctor == null) return NotFound();

            var response = new
            {             Name = doctor.name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name
            };
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetActiveDoctorById(id);
            if (doctor == null) return NotFound();
            doctor.Deactivate();
            return NoContent();
        }
    }
}