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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillComboBox(); //call to the method to fill
            fillDataGridView();
        }

        //method to load departments name
        private void fillComboBox()
        {
            List<string> departments = (
                //query with LINQ
                from department in Department.GetDepartments()
                select department.deparmentName //select the element to return
                ).ToList();

            //fill the combobox
            foreach (var depto in departments)
                cboDepartments.Items.Add(depto);
        }

        //method to ask employee data
        //and the department wich they belong to
        //using operators
        //inside the datagridview it will show:
        //cod employee, full name, 

        private void fillDataGridView()
        {
            //query to join two collections (employee and deparment)
            var joinData = Employee.GetEmployees()
                .Join(
                    Department.GetDepartments(),
                    employee => employee.deparmentId,
                    deparment => deparment.deparmentId,
                    (employee, deparment) => new //anonymos function
                    {
                        //items to select
                        employeeId = employee.employeeId,
                        employeeFullName = employee.firstName + " " + employee.lastName, //concatenamos firstName y lastName
                        employeeHireDate = employee.hire_date.Year, //extraemos el año de fecha de contratacion
                        employeeDeparment = deparment.deparmentName
                    }
                ).ToList();

            //fill dgv
            //add columns
            dgData.Columns.Add("employeeId", "ID EMPLEADO");
            dgData.Columns.Add("employeeFullName", "NOMBRE");
            dgData.Columns.Add("hireDate", "AñO CONTRATACION");
            dgData.Columns.Add("departmentName", "DEPARTAMENTO");

            //add rows
            foreach (var data in joinData)
            {
                dgData.Rows.Add(
                    data.employeeId,
                    data.employeeFullName,
                    data.employeeHireDate,
                    data.employeeDeparment
                    );
            }
        }

    }
}
