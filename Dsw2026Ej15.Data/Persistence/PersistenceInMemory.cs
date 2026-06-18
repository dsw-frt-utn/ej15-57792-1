using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data.Persistence
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Speciality> _specialities;

        public PersistenceInMemory(string specialitiesFilePath)
        {
            _doctors = new List<Doctor>();
            _specialities = LoadSpecialities(specialitiesFilePath);
        }

        public IReadOnlyCollection<Speciality> GetSpecialities() => _specialities.AsReadOnly();

        public Speciality? GetSpecialityById(Guid id) => _specialities.FirstOrDefault(s => s.Id == id);
        

        public IReadOnlyCollection<Doctor> GetDoctors() => _doctors.AsReadOnly();
        

        public IReadOnlyCollection<Doctor> GetActiveDoctors() => _doctors.Where(d => d.IsActive).ToList().AsReadOnly();
        

        public Doctor? GetDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id);
        

        public Doctor? GetActiveDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        

        public void addDoctor(Doctor doctor) => _doctors.Add(doctor);
        
        private List<Speciality> LoadSpecialities(string path)
        {
            if (!File.Exists(path)) return new List<Speciality>();

            var json = File.ReadAllText(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Speciality>>(json, options) ?? new List<Speciality>();
        }
    }
}
