using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exampleAPP
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            lblCod.Text = Form1.cod;

            employeeData();
        }

        private void employeeData()
        {
            var query =
                (
                    from employee in Employee.GetEmployees()
                    join department in Department.GetDepartments() on
                    employee.deparmentId equals department.deparmentId
                    where employee.employeeId == lblCod.Text.ToString()
                    select new
                    {
                        employeeId = employee.employeeId,
                        employeeFullName = employee.firstName + " " + employee.lastName,
                        employeeBirthDate = employee.birthDate,
                        employeeAge = DateTime.Now.Year - employee.birthDate.Year,
                        employeeHireDate = employee.hire_date.Year,
                        employeeEmail = employee.email,
                        employeeDepartment = department.deparmentName
                    }
                );

            //show data in controls
            foreach (var data in query)
            {
                lblNameEmployee.Text = data.employeeFullName;
                lblFullname.Text = data.employeeFullName;
                lblbirthDate.Text = data.employeeBirthDate.ToString();
                lblAge.Text = data.employeeAge.ToString();
                lblhireDate.Text = data.employeeHireDate.ToString();
                lblEmail.Text = data.employeeEmail;
                lblDepto.Text = data.employeeDepartment;
            }

            var titles = Employee.GetEmployees()
                .Where(cod => cod.employeeId == lblCod.Text)
                .SelectMany(title => title.titles);

            foreach (var title in titles)
            {
                lvTitles.Items.Add(title);
            }
        }
    }
}
