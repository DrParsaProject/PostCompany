using PostCompany.Models;
using PostCompany.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.OutputForms
{
    public class LoginOForm
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public EmployeeRole Role { get; set; }
    }
}