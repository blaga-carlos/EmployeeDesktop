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
    internal partial class EditDepartment : Form
    {
        private Department selectedDepartment;
        private DepartmentService departmentService;
        public EditDepartment(Department department)
        {
            InitializeComponent();
            selectedDepartment = department;
            departmentService = new DepartmentService();
            PopulateForm();
        }

        private void PopulateForm()
        {
            txtDescription.Text = selectedDepartment.description;
            txtParentId.Text = selectedDepartment.parentID.ToString();
            txtManagerId.Text = selectedDepartment.managerId.ToString();
        }

        private async void UpdateDepartment()
        {
            Department updatedDepartment = new Department
            {
                departmentId = selectedDepartment.departmentId,
                description = txtDescription.Text,
                parentID = int.Parse(txtParentId.Text),
                managerId = int.Parse(txtManagerId.Text),
            };

            string result = await departmentService.UpdateDepartmentAsync(updatedDepartment);

            MessageBox.Show(result);

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDepartment();
        }
    }
}
