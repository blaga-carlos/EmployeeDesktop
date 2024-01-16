using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    internal partial class EditUser : Form
    {
        private Employee selectedEmployee;
        private EmployeeService employeeService;

        public EditUser(Employee employee)
        {
            InitializeComponent();
            selectedEmployee = employee;
            employeeService = new EmployeeService();
            PopulateForm(); 
        }

        private void PopulateForm()
        {
            textBoxName.Text = selectedEmployee.name;
            textBoxEmail.Text = selectedEmployee.email;
            textBoxManagerId.Text = selectedEmployee.managerId.ToString();
        }

        private async void UpdateEmployee()
        {
            Employee updatedEmployee = new Employee
            {
                Id = selectedEmployee.Id,
                name = textBoxName.Text,
                email = textBoxEmail.Text,
                managerId = int.Parse(textBoxManagerId.Text),
            };

            string result = await employeeService.UpdateEmployeeAsync(updatedEmployee);

            MessageBox.Show(result);

            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdateEmployee();
        }
    }
}
