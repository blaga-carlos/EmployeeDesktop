using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace client
{
    internal class DepartmentService
    {
        static HttpClient client = new HttpClient();

        public DepartmentService()
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

        public List<Department> GetDepartments()
        {
            List<Department> departments = null;
            HttpResponseMessage response = client.GetAsync("/api/getAllDepartments").Result;
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(resultString);
                return departments;
            }
            return null;
        }

        public string SaveDepartment(Department department)
        {
            try
            {
                var departmentToSave = new
                {
                    description = department.description,
                    parentId = department.parentID,
                    managerId = department.managerId,
                    employees = department.Employees.Select(e => new
                    {
                        name = e.name,
                        managerId = e.managerId,
                        email = e.email,
                        password = e.password
                    })
                };

                string departmentJson = JsonConvert.SerializeObject(departmentToSave);

                StringContent content = new StringContent(departmentJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/api/saveDepartment", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return "Department saved successfully!";
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

        public async Task<string> DeleteDepartmentAsync(int departmentId)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"/api/deleteDepartment/{departmentId}");

                if (response.IsSuccessStatusCode)
                {
                    return "Department deleted successfully!";
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

        public async Task<string> UpdateDepartmentAsync(Department updatedDepartment)
        {
            try
            {
                var departmentToSave = new
                {
                    departmentId = updatedDepartment.departmentId,
                    description = updatedDepartment.description,
                    parentId = updatedDepartment.parentID,
                    managerId = updatedDepartment.managerId,
                    // Add other properties as needed
                };

                string departmentJson = JsonConvert.SerializeObject(departmentToSave);

                StringContent content = new StringContent(departmentJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"/api/updateDepartment/{departmentToSave.departmentId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return "Department updated successfully!";
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
