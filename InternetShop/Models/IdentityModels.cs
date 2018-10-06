using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetShop.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [HiddenInput(DisplayValue =false)]
        public override string Id { get => base.Id; set => base.Id = value; }
        [HiddenInput(DisplayValue = false)]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        [HiddenInput(DisplayValue = false)]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        public bool IsLegalEntity { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }
        public string CompanyName { get; set; }
        public string IIN { get; set; }
        public string KPP { get; set; }
        public string BIK { get; set; }
        public string CorAccount { get; set; }
        public string RAccount { get; set; }
        public string BankName { get; set; }
        public string LegalAddress { get; set; }
        public bool LegalAddresIsActual { get; set; }
        public string ContactEntity { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationRole: IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
       
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleGoodMap> SaleGoodMaps { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       // public System.Data.Entity.DbSet<InternetShop.Models.ApplicationUser> ApplicationUsers { get; set; }

        // public System.Data.Entity.DbSet<InternetShop.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}