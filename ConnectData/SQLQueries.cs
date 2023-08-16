using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConstants
    {
    internal class SQLQueries
    {
        public static string CONNSTRING = "server=localhost; port=3306; database=customersbase; uid=root; pwd=040814-Alexey;";
        public static string SELECTALL = "select * from employees";
        public static string INSERT = "INSERT INTO employees (employee_surname, gender) VALUES (@name, @gender)";
        public static string DELETE = "delete from employees where Employee_ID = @employee_id";
        public static string UPDATE = "update employees set employee_surname = @name, gender = @gender where employee_id = @id";
    }
}
