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
        //declare property to send employee's code
        public static string cod = "";

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

        private void btnShow_Click(object sender, EventArgs e)
        {
            //call to the methods to clean the dgv
            clearDataGridView();
            filterByDepartment();

        }

        //method to clean the dgv
        private void clearDataGridView()
        {
            dgData.Columns.Clear();
            dgData.Rows.Clear();
        }


        //method to filter the data in the dgv
        private void filterByDepartment()
        {
            var employeesByDepartment =
                (
                    from employee in Employee.GetEmployees()
                    join department in Department.GetDepartments() on
                    employee.deparmentId equals department.deparmentId //comparision operator
                    where department.deparmentName == cboDepartments.Text //filter operator
                    orderby employee.firstName descending //order names by descending order
                    select new 
                    { 
                        employeeId = employee.employeeId,
                        employeeFullName = employee.firstName  + "" + employee.lastName,
                        employeeAge = DateTime.Now.Year - employee.birthDate.Year,
                        employeeHireDate = employee.hire_date.Year
                    }

                ).ToList();

            //fill
            dgData.Columns.Add("employeeId", "CODIGO EMPLEADO");
            dgData.Columns.Add("employeeFullName", "NOMBRE COMPLETO");
            dgData.Columns.Add("employeeAge", "EDAD");
            dgData.Columns.Add("employeeHireDate", "AñO DE CONTRATO");

            foreach (var data in employeesByDepartment)
                dgData.Rows.Add(
                        data.employeeId,
                        data.employeeFullName,
                        data.employeeAge,
                        data.employeeHireDate
                    );
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            clearDataGridView();
            fillDataGridView();
        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get the value 
            cod = dgData.CurrentRow.Cells[0].Value.ToString();
            EmployeeForm formEmployee = new EmployeeForm();
            formEmployee.Show();
        }
    }
}
