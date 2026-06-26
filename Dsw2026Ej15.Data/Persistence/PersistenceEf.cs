using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Dsw2026Ej15.Data.Context;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Persistence
{
    public class PersistenceEf : IPersistence
    {
        private readonly DswDbContext _context;

        public PersistenceEf(DswDbContext context)
        {
            _context = context;
        }

        public void SeedSpecialities(string jsonPath)
        {
            if (!_context.Specialities.Any())
            {

                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<Speciality>>(json);
                if (specialities != null)
                {
                    _context.Specialities.AddRange(specialities);
                    _context.SaveChanges();
                }
            }
        }
        public IReadOnlyCollection<Speciality> GetSpecialities()
        {
            return _context.Specialities.ToList();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _context.Specialities.FirstOrDefault(x => x.Id == id);
        }

        public IReadOnlyCollection<Doctor> GetDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .ToList();
        }

        public IReadOnlyCollection<Doctor> GetActiveDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToList();
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id);
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public void addDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        
    }
}
