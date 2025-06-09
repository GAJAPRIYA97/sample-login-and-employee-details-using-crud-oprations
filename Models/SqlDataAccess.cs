using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Myisolve.Models
{
    public class SqlDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertEmployee(
            string branchName,
            string employeeName,
            string employeeCode,
            string departmentName,
            DateTime dateOfJoin,
            string email,
            string gender,
            string role,
            string mobileNumber1,
            string status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertEmployeedetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BranchName", branchName);
                cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@DateOfJoin", dateOfJoin);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@MobileNumber1", mobileNumber1);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<properties> GetAllEmployees()
        {
            var employees = new List<properties>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var emp = new properties
                        {
                            BranchName = reader["BranchName"].ToString(),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            EmployeeCode = reader["EmployeeCode"].ToString(),
                            DepartmentName = reader["DepartmentName"].ToString(),
                            DateOfJoin = Convert.ToDateTime(reader["DateOfJoin"]),
                            Email = reader["Email"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Role = reader["Role"].ToString(),
                            MobileNumber1 = reader["MobileNumber1"].ToString(),
                            Status = reader["Status"].ToString()
                        };
                        employees.Add(emp);
                    }
                }
            }

            return employees;
        }

        public properties GetEmployeeByCode(string employeeCode)
        {
            properties emp = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeCode = @EmployeeCode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emp = new properties();
                            emp.EmployeeCode = reader["EmployeeCode"].ToString();
                            emp.BranchName = reader["BranchName"].ToString();
                            emp.EmployeeName = reader["EmployeeName"].ToString();
                            emp.MobileNumber1 = reader["MobileNumber1"].ToString();
                            emp.DepartmentName = reader["DepartmentName"].ToString();
                            emp.DateOfJoin = reader["DateOfJoin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateOfJoin"]);
                            emp.Email = reader["Email"].ToString();
                            emp.Gender = reader["Gender"].ToString();
                            emp.Role = reader["Role"].ToString();
                            emp.Status = reader["Status"].ToString();

                        }
                    }
                }
            }
            return emp;
        }
        public void DeleteEmployee(string employeeCode)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeCode = @EmployeeCode", conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(
    string branchName,
    string employeeName,
    string employeeCode,
    string departmentName,
    DateTime dateOfJoin,
    string email,
    string gender,
    string role,
    string mobileNumber1,
    string status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateEmployeedetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BranchName", branchName);
                cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@DateOfJoin", dateOfJoin);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@MobileNumber1", mobileNumber1);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
