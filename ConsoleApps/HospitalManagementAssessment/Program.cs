
using HospitalManagementAssessment.Models;

{
    static void Main()
    {
        using (var context = new HospitalDbContext())
        {
            // Create
            var newPatient = new Patient { FullName = "John Doe" };
            context.Patients.Add(newPatient);
            context.SaveChanges();

            // Read
            var allPatients = context.Patients.ToList();
            foreach (var patient in allPatients)
            {
                Console.WriteLine($"Patient ID: {patient.PatientId}, Name: {patient.FullName}");
            }

            // Update
            var patientToUpdate = context.Patients.FirstOrDefault(p => p.FullName == "Jid doe");
            if (patientToUpdate != null)
            {
                patientToUpdate.FullName = "Jid";
                context.SaveChanges();
            }

            // Delete
            var patientToDelete = context.Patients.FirstOrDefault(p => p.FullName == "Jid doe");
            if (patientToDelete != null)
            {
                context.Patients.Remove(patientToDelete);
                context.SaveChanges();
            }
        }
    }
}
