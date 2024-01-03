using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementAssessment.Models
{
    public class Patient
    {

        public int PatientId { get; set; }
        public string FullName { get; set; }
       
        public string Email { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
