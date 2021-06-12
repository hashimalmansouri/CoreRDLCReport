using AspNetCore.Reporting;
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

        public IWebHostEnvironment _webHostEnvironment { get; }

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
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
