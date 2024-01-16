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
    internal partial class Assign : Form
    {
        private Employee selectedEmployee;
        private List<Department> departments;
        DepartmentService departmentService;


        public Assign(Employee employee)
        {
            InitializeComponent();
            selectedEmployee = employee;
            departmentService = new DepartmentService();
            departmentService.createConnection();
            departments = new List<Department>();
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            
            departments = departmentService.GetDepartments();

            listBox1.DisplayMember = "description";
            listBox1.ValueMember = "departmentId";
            listBox1.DataSource = departments;

            label2.Text = selectedEmployee.name;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Department selectedDepartment = (Department)listBox1.SelectedItem;

                EmployeeService employeeService = new EmployeeService();
                employeeService.createConnection();
                string result = await employeeService.AssignEmployeeToDepartmentAsync(selectedEmployee.Id, selectedDepartment.departmentId);

                MessageBox.Show(result);

                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a department to assign.");
            }
        }
    }
}
