using HospitalManagementAssessment.Models;
using Microsoft.EntityFrameworkCore;

public class HospitalDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DiJ; Database=MyHospital; Encrypt=True; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     //patient
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.PatientId);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Room)
            .WithMany(r => r.Patients)
            .HasForeignKey(p => p.RoomId);
        //Doctor
        modelBuilder.Entity<Doctor>()
            .HasKey(d => d.DoctorId);
        //Appointment
        modelBuilder.Entity<Appointment>()
            .HasKey(a => a.AppointmentId);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId);
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId);
        //Room
        modelBuilder.Entity<Room>()
            .HasKey(r => r.RoomId);

        
    }
}
