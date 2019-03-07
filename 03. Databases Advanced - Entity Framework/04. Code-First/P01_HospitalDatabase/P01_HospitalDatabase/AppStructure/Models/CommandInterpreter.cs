namespace P01_HospitalDatabase.AppStructure.Models
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Text;

    public class CommandInterpreter
    {
        public string Command1()
        {
            using (var context = new HospitalContext())
            {
                var sb = new StringBuilder();

                string patientName = Console.ReadLine();

                var patient = context.Patients
                    .Where(p => p.FirstName + " " + p.LastName == patientName)
                    .Include(x => x.Visitations)
                    .Include(x => x.Diagnoses)
                    .Include(x => x.Prescriptions)
                    .FirstOrDefault();

                if (patient == null)
                {
                    sb.AppendLine("Patient with that name doesn't exists!");
                    return sb.ToString().TrimEnd();
                }

                sb.AppendLine(
                    $"Full name: {patient.FirstName} {patient.LastName}, address: {patient.Address}, email: {patient.Email}, insurance: {(patient.HasInsurance ? "yes" : "no")}");

                sb.AppendLine("Visitations:");
                if (patient.Visitations == null)
                {
                    sb.AppendLine("Patient don't have visitations.");
                }
                else
                {
                    foreach (var visitation in patient.Visitations)
                    {
                        var doctor = context.Doctors.FirstOrDefault(x => x.DoctorId == visitation.DoctorId);
                        string comments = visitation.Comments == null ? "" : " " + visitation.Comments;
                        sb.AppendLine($"{visitation.Date}, {doctor.Name}{comments}.");
                    }
                }

                sb.AppendLine($"Diagnoses: {string.Join(", ", patient.Diagnoses.Select(d => d.Name))}");

                sb.AppendLine("Medicaments:");
                if (patient.Prescriptions == null)
                {
                    sb.AppendLine("Patient don't have medicaments.");
                }
                else
                {
                    foreach (var prescription in patient.Prescriptions)
                    {
                        var medicament =
                            context.Medicaments.FirstOrDefault(m => m.MedicamentId == prescription.MedicamentId);
                        sb.AppendLine($"{medicament.Name}");
                    }
                }

                return sb.ToString().TrimEnd();
            }
        }

        public void Command2()
        {

        }

        public void Command3()
        {

        }

        public void Command4()
        {

        }

        public void Command5()
        {

        }

        public void Command6()
        {
            Environment.Exit(0);
        }
    }
}
