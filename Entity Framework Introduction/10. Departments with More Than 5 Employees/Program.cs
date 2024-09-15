﻿using System.Net.Mime;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni{


public class Program{
    public static void Main(string[] args){
        SoftUniContext context = new SoftUniContext();


        Console.WriteLine(StartUp.GetDepartmentsWithMoreThan5Employees(context)) ;
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
    

     public static string AddNewAddressToEmployee(SoftUniContext context){

        var address = new Address();

        address.AddressText = "Vitoshka 15";
        address.TownId = 4;

        context.Addresses.Add(address);


        var employee = context.Employees.Include(e => e.Address).Where(x => x.LastName == "Nakov").First();

        employee.Address = address; // if error change the addressid too 
        employee.AddressId = address.AddressId;

        context.SaveChanges();

        var employees = context.Employees.OrderByDescending(x => x.AddressId).Take(10);
        List<string> final = new List<string>();


        foreach (var employee_f in employees){
            final.Add(employee_f.Address.AddressText);
        }

        return string.Join("\n", final);
     }



    public static string GetEmployeesInPeriod(SoftUniContext context){ 

        var employees = context.Employees
        .Include(em => em.Manager).Include(em => em.EmployeesProjects).ThenInclude(em => em.Project)
        .Take(10);

        List<string> final = new List<string>();

        foreach(var employee in employees){

            final.Add($"{employee.FirstName} {employee.LastName} - Manager: {employee.Manager.FirstName} {employee.Manager.LastName}");


            foreach (var project in employee.EmployeesProjects){

                if (project.Project.StartDate.Year < 2004 && project.Project.StartDate.Year > 2000 ){
                    final.Add($"--{project.Project.Name} - {project.Project.StartDate} - {(project.Project.EndDate != null ? project.Project.EndDate : "not finished") }");
                }

            }
        }

        return string.Join('\n', final);
    }



    public static string GetAddressesByTown(SoftUniContext context){

        var addresses = context.Addresses
        .Include(a => a.Employees).Include(a => a.Town)
        .OrderByDescending(a => a.Employees.Count()).ThenBy(a => a.Town.Name).ThenBy(a => a.AddressText)
        .Take(10);

        List<string> strings = new List<string>();

        foreach (var address in addresses){

            strings.Add($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count()} employees");
        }


        return string.Join('\n', strings);
    }


    public static string GetEmployee147(SoftUniContext context){

        var employee = context.Employees.Include(x => x.EmployeesProjects).ThenInclude(x => x.Project)
        .FirstOrDefault(x => x.EmployeeId == 147);

        List<string> final = new List<string>
        {
            $"{employee.FirstName} {employee.LastName} - {employee.JobTitle}"
        };
        

        var projects = employee.EmployeesProjects.OrderBy(x => x.Project.Name);

        foreach (var project in projects){

            final.Add(project.Project.Name);

        }

        return string.Join("\n", final);
    }


    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context){


        var departs = context.Departments
        .Include(dp => dp.Manager).Include(dp => dp.Employees)
        .Where(dp => dp.Employees.Count > 5)
        .OrderBy(dp => dp.Employees.Count).ThenBy(dp => dp.Name);  


        List<string> final = new List<string>();

        foreach (var depart in departs){

            final.Add($"{depart.Name} - {depart.Manager.FirstName}  {depart.Manager.LastName}");

            var employees = depart.Employees.OrderBy(em => em.FirstName).ThenBy(em => em.LastName);

            foreach(var employee in employees){

                final.Add($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            }

        }

        return string.Join('\n', final);


    }

}