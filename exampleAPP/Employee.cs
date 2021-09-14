using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exampleAPP
{
    class Employee
    {
        public string employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime hire_date { get; set; }
        public int deparmentId { get; set; }
        public List<string> titles { get; set; }



        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee { employeeId="E1001", firstName="Jose", lastName="Funes Lopez", email="j@gmail.com", birthDate=new DateTime(1988,05,03),
                    hire_date= new DateTime(2000,08,06), deparmentId=2001, titles= new List<string>{"Lic. en Contabilidad" } },

                new Employee { employeeId="E1002", firstName="Andrea", lastName="Robles", email="ar@gmail.com", birthDate=new DateTime(1981,10,10),
                    hire_date= new DateTime(2001,05,01), deparmentId=2002,titles= new List<string>{"Lic. en Contabilidad", "Lic. en Admon. de Empresas" }},

                new Employee { employeeId="E1003", firstName="Carmen", lastName="Gomez", email="cgomez@gmail.com", birthDate=new DateTime(1975,02,01),
                    hire_date= new DateTime(2002,12,12), deparmentId=2003, titles= new List<string>{"Lic. en Admon. de Empresas", "Tecnico en Sistemas" }},

                new Employee { employeeId="E1004", firstName="Andres", lastName="Valladares", email="ava@gmail.com", birthDate=new DateTime(1999,05,03),
                    hire_date= new DateTime(2000,03,01), deparmentId=2003, titles= new List<string>{"Lic. en Derecho" }},

                new Employee { employeeId="E1005", firstName="Roberto", lastName="Luna", email="lunaroberto@gmail.com", birthDate=new DateTime(1980,09,22),
                    hire_date= new DateTime(2003,02,04), deparmentId=2001, titles= new List<string>{"Lic. en Contabilidad" }},

            };

            return employees;
        }
    }
}
