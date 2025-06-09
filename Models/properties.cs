using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myisolve.Models
{
   
    public class properties
    {

       
            public int id { get; set; }

            [Required(ErrorMessage = "Employee Name is required.")]
            [StringLength(100)]
            public string EmployeeName { get; set; }

            [Required(ErrorMessage = "Employee Code is required.")]
            [StringLength(50)]
            public string EmployeeCode { get; set; }

            [Required(ErrorMessage = "Branch Name is required.")]
            [StringLength(100)]
            public string BranchName { get; set; }

            [Required(ErrorMessage = "Department is required.")]
            [StringLength(100)]
            public string DepartmentName { get; set; }

            public string Area { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Enter a valid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Qualification is required.")]
            public string Qualification { get; set; }

            [Required(ErrorMessage = "Mobile number is required.")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
            public string MobileNumber1 { get; set; }

            [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
            public string Mobile_No2 { get; set; }

            [DataType(DataType.Date)]
            public string Date_of_Birth { get; set; }

            [Required(ErrorMessage = "City is required.")]
            public string City { get; set; }

            public string State { get; set; }

            [StringLength(50)]
            public string Username { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Role is required.")]
            public string Role { get; set; }

            public string Designation { get; set; }

            [Required(ErrorMessage = "Gender is required.")]
            public string Gender { get; set; }

            [Required(ErrorMessage = "Date of join is required.")]
            [DataType(DataType.Date)]
            public DateTime? DateOfJoin { get; set; }

            [Required(ErrorMessage = "Pincode is required.")]
            [Range(100000, 999999, ErrorMessage = "Enter a valid 6-digit pincode.")]
            public int Pincode { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            public string Status { get; set; }
        }
    }

