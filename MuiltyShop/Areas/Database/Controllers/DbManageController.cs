using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MuiltyShop.Models.Product;
using MuiltyShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MuiltyShop.Models.Product.Category;

namespace MuiltyShop.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public DbManageController(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();

            StatusMessage = success ? "Xóa Database thành công" : "Không xóa được Db";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();

            StatusMessage = "Cập nhật Database thành công";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            // Create Roles
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolenames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename));
                }
            }

            // admin, pass=admin123, admin@example.com
            var useradmin = await _userManager.FindByEmailAsync("admin@example.com");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(useradmin, "123456");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
                await _signInManager.SignInAsync(useradmin, false);
                return RedirectToAction(nameof(SeedDataAsync));
            }
            else
            {
                var user = await _userManager.GetUserAsync(this.User);
                if (user == null) return this.Forbid();
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Any(r => r == RoleName.Administrator))
                {
                    return this.Forbid(); // Cần cập nhật
                }
            }
            SeedProductCategory();

            StatusMessage = "Vừa seed Database";
            return RedirectToAction("Index");


        }

        private void SeedProductCategory()
        {

            _dbContext.CategoryProducts.RemoveRange(_dbContext.CategoryProducts.Where(c => c.Description.Contains("[fakeData]")));
            _dbContext.Products.RemoveRange(_dbContext.Products.Where(p => p.Content.Contains("[fakeData]")));

            _dbContext.SaveChanges();

            var fakerCategory = new Faker<CategoryProductModel>();
            int cm = 1;
            fakerCategory.RuleFor(c => c.Title, fk => $"Nhom SP{cm++} " + fk.Lorem.Sentence(1, 2).Trim('.'));
            fakerCategory.RuleFor(c => c.Description, fk => fk.Lorem.Sentences(5) + "[fakeData]");
            fakerCategory.RuleFor(c => c.Slug, fk => fk.Lorem.Slug());



            var cate1 = fakerCategory.Generate();
            var cate11 = fakerCategory.Generate();
            var cate12 = fakerCategory.Generate();
            var cate2 = fakerCategory.Generate();
            var cate21 = fakerCategory.Generate();
            var cate211 = fakerCategory.Generate();


            cate11.ParentCategory = cate1;
            cate12.ParentCategory = cate1;
            cate21.ParentCategory = cate2;
            cate211.ParentCategory = cate21;

            var categories = new CategoryProductModel[] { cate1, cate2, cate12, cate11, cate21, cate211 };
            _dbContext.CategoryProducts.AddRange(categories);



            // POST
            var rCateIndex = new Random();
            int bv = 1;

            var user = _userManager.GetUserAsync(this.User).Result;
            var fakerProduct = new Faker<ProductModel>();
            fakerProduct.RuleFor(p => p.AuthorId, f => user.Id);
            fakerProduct.RuleFor(p => p.Content, f => f.Commerce.ProductDescription() + "[fakeData]");
            fakerProduct.RuleFor(p => p.DateCreated, f => f.Date.Between(new DateTime(2021, 1, 1), new DateTime(2021, 7, 1)));
            fakerProduct.RuleFor(p => p.Description, f => f.Lorem.Sentences(3));
            fakerProduct.RuleFor(p => p.Published, f => true);
            fakerProduct.RuleFor(p => p.Slug, f => f.Lorem.Slug());
            fakerProduct.RuleFor(p => p.Title, f => $"SP {bv++} " + f.Commerce.ProductName());
            fakerProduct.RuleFor(p => p.Price, f => int.Parse(f.Commerce.Price(500, 1000, 0)));

            List<ProductModel> products = new List<ProductModel>();
            List<ProductCategoryProductModel> product_categories = new List<ProductCategoryProductModel>();


            for (int i = 0; i < 40; i++)
            {
                var product = fakerProduct.Generate();
                product.DateUpdated = product.DateCreated;
                products.Add(product);
                product_categories.Add(new ProductCategoryProductModel()
                {
                    Product = product,
                    Category = categories[rCateIndex.Next(5)]
                });
            }

            _dbContext.AddRange(products);
            _dbContext.AddRange(product_categories);
            // END POST

            _dbContext.SaveChanges();
        }
    }
}
