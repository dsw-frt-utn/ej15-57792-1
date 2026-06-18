using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Speciality Speciality { get; set; } = null!;
        
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
