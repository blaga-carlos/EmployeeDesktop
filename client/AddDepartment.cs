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
    public partial class AddDepartment : Form
    {
        private DepartmentService departmentService;
        public AddDepartment()
        {
            InitializeComponent();
            departmentService = new DepartmentService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string description = txtDescription.Text;
            int parentID;
            if (!int.TryParse(txtParentID.Text, out parentID))
            {
                MessageBox.Show("Invalid Parent ID. Please enter a valid integer.");
                return;
            }

            int managerId;
            if (!int.TryParse(txtManagerId.Text, out managerId))
            {
                MessageBox.Show("Invalid Manager ID. Please enter a valid integer.");
                return;
            }

            Department newDepartment = new Department
            {
                description = description,
                parentID = parentID,
                managerId = managerId,
                Employees = new List<Employee>() 
            };

            string result = departmentService.SaveDepartment(newDepartment);

            MessageBox.Show(result);

            if (result == "Department saved successfully!")
            {
                this.Close();
            }
        }
    }
}
