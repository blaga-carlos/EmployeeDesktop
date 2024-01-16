using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json; 



namespace client
{
    internal class EmployeeService
    {
        static HttpClient client = new HttpClient();

        public EmployeeService()
        {
            client = new HttpClient();
            createConnection();
        }
        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8080");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = null;
            HttpResponseMessage response = client.GetAsync("/api/getAllEmployees").Result;
            if (response.IsSuccessStatusCode) 
            { 
                string resultString = response.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(resultString);
                return employees;
            }
            return null;
        }

        public string SaveEmployee(Employee employee)
        {
            try
            {
                var employeeToSave = new
                {
                    id = employee.Id,
                    name = employee.name,
                    managerId = employee.managerId,
                    email = employee.email,
                    password = employee.password
                };

                string employeeJson = JsonConvert.SerializeObject(employeeToSave);

                StringContent content = new StringContent(employeeJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/api/saveEmployee", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return "Employee saved successfully!";
                }
                else
                {
                    string error = response.Content.ReadAsStringAsync().Result;
                    return $"Error: {response.StatusCode}, {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> UpdateEmployeeAsync(Employee updatedEmployee)
        {
            try
            {
                var employeeToSave = new
                {
                    id = updatedEmployee.Id,
                    name = updatedEmployee.name,
                    managerId = updatedEmployee.managerId,
                    email = updatedEmployee.email,
                    password = updatedEmployee.password
                };

                Console.WriteLine($"UpdateEmployeeAsync: Employee ID: {employeeToSave.id}");  //

                string employeeJson = JsonConvert.SerializeObject(employeeToSave);

                StringContent content = new StringContent(employeeJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"/api/updateEmployee/{employeeToSave.id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return "Employee updated successfully!";
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}, {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"/api/deleteEmployee/{employeeId}");

                if (response.IsSuccessStatusCode)
                {
                    return "Employee deleted successfully!";
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}, {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> AssignEmployeeToDepartmentAsync(int employeeId, int departmentId)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync($"/api/assignEmployeeToDepartment/{employeeId}/{departmentId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return "Employee assigned to department successfully!";
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}, {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


    }
}
