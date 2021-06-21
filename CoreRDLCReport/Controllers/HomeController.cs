using AspNetCore.Reporting;
using CoreRDLCReport.Data;
using CoreRDLCReport.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRDLCReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoreRDLCReportContext _context;
        public IWebHostEnvironment _webHostEnvironment { get; }

        public HomeController(IWebHostEnvironment webHostEnvironment,
            CoreRDLCReportContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Print()
        {
            var dt = GetEmployeeList();
            string mimetype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptEmployee.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployee", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult DepartmentReport()
        {
            var dt = GetDepartmentList();
            string mimetype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptDepartment.rdlc";
            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsDepartment", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, null, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult DeptEmpReport()
        {
            var dt = GetDeptEmpList();
            string mimetype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptDeptEmp.rdlc";
            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, null, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        private DataTable GetDeptEmpList()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Phone");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Department");
            foreach (var item in _context.Department)
            {
                dt.Rows.Add(item.Name, item.Phone, "AAA", "BBB");
            }
            //var obj = new[] { "A", "B", "C", "D" };
            //dt.Rows.Add(obj);
            //for (int i = 0; i < 20; i++)
            //{
            //    dt.Rows.Add("Computer Science", "05333666", "Hashim Almansouri", "IT");
            //}
            return dt;
        }

        private DataTable GetDepartmentList()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Phone");

            DataRow row;
            var departments = _context.Department.ToList();
            foreach (var item in departments)
            {
                row = dt.NewRow();
                row["Id"] = item.Id;
                row["Name"] = item.Name;
                row["Phone"] = item.Phone;
                dt.Rows.Add(row);
            }
            return dt;
        }

        private DataTable GetEmployeeList()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Department");

            DataRow row;
            for (int i = 101; i < 150; i++)
            {
                row = dt.NewRow();
                row["EmpId"] = i;
                row["EmpName"] = "Hashim Al-Mansouri " + i;
                row["Phone"] = "700711" + i;
                row["Department"] = "Computer Science";
                dt.Rows.Add(row);
            }
            return dt;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
