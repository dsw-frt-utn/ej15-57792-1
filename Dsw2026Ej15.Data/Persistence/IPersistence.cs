using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data.Persistence
{
    public interface IPersistence
    {
        IReadOnlyCollection<Speciality> GetSpecialities();
        Speciality? GetSpecialityById(Guid id);

        IReadOnlyCollection<Doctor> GetDoctors();
        Doctor? GetDoctorById(Guid id);

        void addDoctor(Doctor doctor);
    }
}
