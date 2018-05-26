using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Contract.Data.Models;

namespace Contract.Data
{
    public class MainContext : DbContext
    {
        static MainContext()
        {
            Database.SetInitializer(new MainInitializer());
        }

        public MainContext()
            : base("MainContext")
        { 
        }

        public DbSet<ActInvoice> ActInvoices { get; set; }
        public DbSet<Organization> Organiations { get; set; }
        public DbSet<BranchUser> BranchUsers { get; set; }
        public DbSet<Models.Contract> Contracts { get; set; }
        public DbSet<ContractDepartment> ContractDepartments { get; set; }
        public DbSet<ContractWorkPayment> ContractWorkPayments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Nakladnoy> Nakladnoys { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Period> Periods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
        }
    }
}