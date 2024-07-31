
using System.Collections;
using System.Text.Json.Serialization;
using System.Text;

namespace DTOs
{
    public  class User : BaseDTO
    {
        public int Dni { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 

        [JsonIgnore]
        public byte[] PasswordBytes
        {
            get => Encoding.UTF8.GetBytes(Password); 
            set => Password = Encoding.UTF8.GetString(value); 
        }
        public string LastName { get; set; }
        public string  Address { get; set; }
        public DateTime BirthDate { get; set; }

        public int PhoneNumber { get; set; }
  
        public char Gender { get; set; }
       


    }
}
