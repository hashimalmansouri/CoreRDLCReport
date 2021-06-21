using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreRDLCReport.Models;

namespace CoreRDLCReport.Data
{
    public class CoreRDLCReportContext : DbContext
    {
        public CoreRDLCReportContext (DbContextOptions<CoreRDLCReportContext> options)
            : base(options)
        {
        }

        public DbSet<CoreRDLCReport.Models.Department> Department { get; set; }
        public DbSet<CoreRDLCReport.Models.Employee> Employees { get; set; }
    }
}
