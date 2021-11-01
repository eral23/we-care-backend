using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Dto
{
    public class ApplicationUserDto
    {
        // TBD
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
    public class ApplicationUserRegisterPatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ApplicationUserRegisterSpecialistDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Area { get; set; }
        public string TuitionNumber { get; set; }
        public string Password { get; set; }
    }
    public class ApplicationUserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
