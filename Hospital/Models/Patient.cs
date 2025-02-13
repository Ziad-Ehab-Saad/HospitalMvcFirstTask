namespace Hospital.Models
{
    public class Patient
    {
        public int id { get; set; }
        public string Name { get; set; }
        
        private DateOnly _appointmentDate;
        public DateOnly AppointmentDate
        {
            get => _appointmentDate;
            set
            {
                if (value <= DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("Appointment date must be in the future.");
                }
                _appointmentDate = value;
            }
        }
        public TimeOnly AppointmentTime { get; set; }

        public int DoctorId  { get; set; }
        public Doctor doctor { get; set; }


    }
}
