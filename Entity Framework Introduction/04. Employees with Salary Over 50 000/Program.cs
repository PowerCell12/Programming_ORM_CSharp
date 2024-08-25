using Microsoft.EntityFrameworkCore.Internal;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni{


public class Program{
    public static void Main(string[] args){
        SoftUniContext context = new SoftUniContext();


        Console.WriteLine(StartUp.GetEmployeesWithSalaryOver50000(context)) ;
    }
}
}




public class StartUp{


    public static string GetEmployeesFullInformation(SoftUniContext context){

        var array = new List<string>();
        var employees = context.Employees.OrderBy(x => x.EmployeeId);

        foreach (var employee in employees){

            array.Add($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");

        }

        return string.Join("\n", array);
    }


     public static string GetEmployeesWithSalaryOver50000(SoftUniContext context){

        var employees = context.Employees.Where(x => x.Salary > 50000).OrderBy(x => x.FirstName).ToList();
        List<string> final = new List<string>();

        foreach (var employee in employees){
            final.Add($"{employee.FirstName} - {employee.Salary:F2}");
        }

        return string.Join("\n", final);



     }

}
