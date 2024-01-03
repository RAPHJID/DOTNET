using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementAssessment.Models
{
    public class Room
    {

        public int RoomId { get; set; }
        public int RoomNumber { get; set; }

        public int PatientId { get; set; }
        public List<Patient> Patients { get; set; }
        public Patient Patient { get; set; }
    }
}
