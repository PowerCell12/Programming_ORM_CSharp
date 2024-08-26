using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni{


public class Program{
    public static void Main(string[] args){
        SoftUniContext context = new SoftUniContext();


        Console.WriteLine(StartUp.GetEmployeesFromResearchAndDevelopment(context)) ;
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



    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context){

        var employees = context.Employees.Include(employee => employee.Department)  
        .Where(employee => employee.Department.Name == "Research and Development")
        .OrderBy(employee => employee.Salary)
        .ThenByDescending(employee => employee.FirstName).ToList();   


        List<string> final = new List<string>();

        foreach (var employee in employees){

            final.Add($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:F2}");


        }

        return string.Join("\n", final);

    }
    

}
