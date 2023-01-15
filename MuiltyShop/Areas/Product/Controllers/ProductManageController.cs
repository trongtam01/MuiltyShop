using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using MuiltyShop.Areas.Product.Models;
using MuiltyShop.Models;
using MuiltyShop.Models.Photo;
using MuiltyShop.Models.Product;
using MuiltyShop.Models.Product.Category;
using MuiltyShop.Utilities;
using System;

namespace MuiltyShop.Areas.Product.Controllers
{
    [Area("Product")]
    [Route("admin/productmanage/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class ProductManageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ProductManageController> _logger;

        public ProductManageController(AppDbContext context, UserManager<AppUser> userManager, ILogger<ProductManageController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Product/ProductManage
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var products = _context.Products
                        .Include(p => p.Author)
                        .OrderByDescending(p => p.DateUpdated);

            int totalPosts = await products.CountAsync();
            if (pagesize <= 0) pagesize = 10;
            int countPages = (int)Math.Ceiling((double)totalPosts / pagesize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pagesize = pagesize
                })
            };

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalPosts = totalPosts;

            ViewBag.postIndex = (currentPage - 1) * pagesize;

            var productInPage = await products.Skip((currentPage - 1) * pagesize)
                             .Take(pagesize)
                             .Include(p => p.ProductCategoryProducts)
                             .ThenInclude(pc => pc.Category)
                             .ToListAsync();

            return View(productInPage);
        }

        // GET: Product/ProductManage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/ProductManage/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _context.CategoryProducts.ToListAsync();

            ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");

            return View();
        }

        // POST: Product/ProductManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Slug,Content,Published,CategoryIDs, Price")] CreateProductModel product)
        {
            var categories = await _context.CategoryProducts.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");

            if (product.Slug == null)
            {
                product.Slug = AppUtilities.GenerateSlug(product.Title);
            }

            if (await _context.Products.AnyAsync(p => p.Slug == product.Slug))
            {
                ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                return View(product);
            }



            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                product.DateCreated = product.DateUpdated = DateTime.Now;
                product.AuthorId = user.Id;
                _context.Add(product);

                if (product.CategoryIDs != null)
                {
                    foreach (var CateId in product.CategoryIDs)
                    {
                        _context.Add(new ProductCategoryProductModel()
                        {
                            CategoryID = CateId,
                            Product = product
                        });
                    }
                }


                await _context.SaveChangesAsync();
                StatusMessage = "Vừa tạo bài viết mới";
                return RedirectToAction(nameof(Index));
            }


            return View(product);
        }

        // GET: Product/ProductManage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var product = await _context.Posts.FindAsync(id);
            var product = await _context.Products.Include(p => p.ProductCategoryProducts).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            var postEdit = new CreateProductModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Content = product.Content,
                Description = product.Description,
                Slug = product.Slug,
                Published = product.Published,
                CategoryIDs = product.ProductCategoryProducts.Select(pc => pc.CategoryID).ToArray(),
                Price = product.Price
            };

            var categories = await _context.CategoryProducts.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");

            return View(postEdit);
        }

        // POST: Product/ProductManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Description,Slug,Content,Published,CategoryIDs, Price")] CreateProductModel product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            var categories = await _context.CategoryProducts.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");


            if (product.Slug == null)
            {
                product.Slug = AppUtilities.GenerateSlug(product.Title);
            }

            if (await _context.Products.AnyAsync(p => p.Slug == product.Slug && p.ProductId != id))
            {
                ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                return View(product);
            }


            if (ModelState.IsValid)
            {
                try
                {

                    var productUpdate = await _context.Products.Include(p => p.ProductCategoryProducts).FirstOrDefaultAsync(p => p.ProductId == id);
                    if (productUpdate == null)
                    {
                        return NotFound();
                    }

                    productUpdate.Title = product.Title;
                    productUpdate.Description = product.Description;
                    productUpdate.Content = product.Content;
                    productUpdate.Published = product.Published;
                    productUpdate.Slug = product.Slug;
                    productUpdate.DateUpdated = DateTime.Now;
                    productUpdate.Price = product.Price;

                    // Update PostCategory
                    if (product.CategoryIDs == null) product.CategoryIDs = new int[] { };

                    var oldCateIds = productUpdate.ProductCategoryProducts.Select(c => c.CategoryID).ToArray();
                    var newCateIds = product.CategoryIDs;

                    var removeCatePosts = from productCate in productUpdate.ProductCategoryProducts
                                          where (!newCateIds.Contains(productCate.CategoryID))
                                          select productCate;
                    _context.ProductCategoryProducts.RemoveRange(removeCatePosts);

                    var addCateIds = from CateId in newCateIds
                                     where !oldCateIds.Contains(CateId)
                                     select CateId;

                    foreach (var CateId in addCateIds)
                    {
                        _context.ProductCategoryProducts.Add(new ProductCategoryProductModel()
                        {
                            ProductID = id,
                            CategoryID = CateId
                        });
                    }

                    _context.Update(productUpdate);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                StatusMessage = "Vừa cập nhật bài viết";
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", product.AuthorId);
            return View(product);
        }

        // GET: Product/ProductManage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/ProductManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            StatusMessage = "Bạn vừa xóa bài viết: " + product.Title;

            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        [HttpGet]
        public IActionResult UploadPhoto(int id)
        {
            var product = _context.Products.Where(e => e.ProductId == id)
                            .Include(p => p.Photos)
                            .FirstOrDefault();
            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }
            ViewData["product"] = product;
            return View(new UploadOneFile());
        }

        [HttpPost, ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(int id, [Bind("FileUpload")] UploadOneFile f)
        {
            var product = _context.Products.Where(e => e.ProductId == id)
                .Include(p => p.Photos)
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }
            ViewData["product"] = product;

            if (f != null)
            {
                var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                            + Path.GetExtension(f.FileUpload.FileName);

                var file = Path.Combine("Uploads", "Products", file1);

                using (var filestream = new FileStream(file, FileMode.Create))
                {
                    await f.FileUpload.CopyToAsync(filestream);
                }

                _context.Add(new PhotoModel()
                {
                    ProductID = product.ProductId,
                    FileName = file1
                });

                await _context.SaveChangesAsync();
            }

            return View(f);
        }

        [HttpPost]
        public IActionResult ListPhotos(int id)
        {
            var product = _context.Products.Where(e => e.ProductId == id)
                .Include(p => p.Photos)
                .FirstOrDefault();

            if (product == null)
            {
                return Json(
                    new
                    {
                        success = 0,
                        message = "Product not found",
                    }
                );
            }

            var listphotos = product.Photos.Select(photo => new
            {
                id = photo.Id,
                path = "/contents/Products/" + photo.FileName
            });

            return Json(
                new
                {
                    success = 1,
                    photos = listphotos
                }
            );
        }

        [HttpPost]
        public IActionResult DeletePhoto(int id)
        {
            var photo = _context.Photos.Where(p => p.Id == id).FirstOrDefault();
            if (photo != null)
            {
                _context.Remove(photo);
                _context.SaveChanges();

                var filename = "Uploads/Products/" + photo.FileName;
                System.IO.File.Delete(filename);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhotoApi(int id, [Bind("FileUpload")] UploadOneFile f)
        {
            var product = _context.Products.Where(e => e.ProductId == id)
                .Include(p => p.Photos)
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }


            if (f != null)
            {
                var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                            + Path.GetExtension(f.FileUpload.FileName);

                var file = Path.Combine("Uploads", "Products", file1);

                using (var filestream = new FileStream(file, FileMode.Create))
                {
                    await f.FileUpload.CopyToAsync(filestream);
                }

                _context.Add(new PhotoModel()
                {
                    ProductID = product.ProductId,
                    FileName = file1
                });

                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
