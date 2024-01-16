using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace client
{
    public partial class AddUser : Form
    {
        private EmployeeService employeeService;

        public AddUser()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Employee newEmployee = new Employee
            {
                Id = int.Parse(txtId.Text),
                name = txtName.Text,
                managerId = int.Parse(txtManagerId.Text),
                email = txtEmail.Text,
                password = txtPassword.Text,
            };

            string result = employeeService.SaveEmployee(newEmployee);

            MessageBox.Show(result);

            this.Close();

        }
    }
}
