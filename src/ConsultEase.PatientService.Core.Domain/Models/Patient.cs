namespace PatientService.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Business logic related to patients can be added here.

        public void UpdateContactInfo(string email, string phoneNumber)
        {
            // Ensure valid email and phone number formats
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
