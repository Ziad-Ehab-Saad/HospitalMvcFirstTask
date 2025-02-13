using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital.Controllers
{
    public class DoctorsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookAppointment()
        {
            var doctorsData = context.doctors.ToList();
            return View("BookAppointment", doctorsData);
        }

        public IActionResult CompleteAppointment(int id)
        {
            var doctorData = context.doctors.FirstOrDefault(e => e.Id == id);
            return View(doctorData);
        }
       
        public IActionResult AddAppointment(string name, DateOnly date, TimeOnly time, int doctorId)
        {
            try
            {
                if (date < DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("Appointment date must be in the future.");
                }

                var patient = new Patient
                {
                    Name = name,
                    AppointmentDate = date,
                    AppointmentTime = time,
                    DoctorId = doctorId
                };

                context.patients.Add(patient);
                context.SaveChanges();

                return RedirectToAction("ListAppointments");
            }
            catch (ArgumentException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                var doctor = context.doctors.FirstOrDefault(d => d.Id == doctorId); 
                return View("CompleteAppointment", doctor); 
            }
        }


        public IActionResult ListAppointments()
        {
            var appointments = context.patients.Include(e=>e.doctor).ToList(); 
            return View(appointments); 
        }


    }
}
