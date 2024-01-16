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
    public partial class Form1 : Form
    {
        EmployeeService employeeService;
        List<Employee> employees;
        DepartmentService departmentService;
        List<Department> departments;
        public Form1()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            employeeService.createConnection();
            departmentService = new DepartmentService();
            departmentService.createConnection();

            LoadEmployeeData();
            LoadDepartmentData();
        }

        private void LoadEmployeeData()
        {
            employees = employeeService.GetEmployees();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = employees;
        }

        private void LoadDepartmentData()
        {
            departments = departmentService.GetDepartments();
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = departments;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
          
                Employee selectedEmployee = employees[e.RowIndex];

                EditUser EditUserForm = new EditUser(selectedEmployee);
                EditUserForm.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser AddUserForm = new AddUser();
            AddUserForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddDepartment AddDepartmentForm = new AddDepartment();
            AddDepartmentForm.Show();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                if (rowIndex >= 0)
                {
                    Employee selectedEmployee = employees[rowIndex];

                    DialogResult result = MessageBox.Show($"Are you sure you want to delete {selectedEmployee.name}?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        string deleteResult = await employeeService.DeleteEmployeeAsync(selectedEmployee.Id);

                        MessageBox.Show(deleteResult);

                        LoadEmployeeData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView2.SelectedCells[0].RowIndex;

                if (rowIndex >= 0)
                {
                    Department selectedDepartment = departments[rowIndex];

                    DialogResult result = MessageBox.Show($"Are you sure you want to delete {selectedDepartment.description}?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        string deleteResult = await departmentService.DeleteDepartmentAsync(selectedDepartment.departmentId);

                        MessageBox.Show(deleteResult);

                        LoadDepartmentData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a department to delete.");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                Department selectedDepartment = departments[e.RowIndex];

                EditDepartment EditDepartmentForm = new EditDepartment(selectedDepartment);
                EditDepartmentForm.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                if (rowIndex >= 0)
                {
                    Employee selectedEmployee = employees[rowIndex];

                    Assign assignForm = new Assign(selectedEmployee);
                    assignForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to assign.");
            }
        }
    }
}
