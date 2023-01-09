using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuiltyShop.Models;
using MuiltyShop.Models.Product.Size;

namespace MuiltyShop.Areas.Product.Controllers
{
    [Area("Product")]
    [Route("admin/productsize/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product/Size
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sizes.ToListAsync());
        }

        // GET: Product/Size/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeModel = await _context.Sizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sizeModel == null)
            {
                return NotFound();
            }

            return View(sizeModel);
        }

        // GET: Product/Size/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Size/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] SizeModel sizeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sizeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sizeModel);
        }

        // GET: Product/Size/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeModel = await _context.Sizes.FindAsync(id);
            if (sizeModel == null)
            {
                return NotFound();
            }
            return View(sizeModel);
        }

        // POST: Product/Size/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] SizeModel sizeModel)
        {
            if (id != sizeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sizeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeModelExists(sizeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sizeModel);
        }

        // GET: Product/Size/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeModel = await _context.Sizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sizeModel == null)
            {
                return NotFound();
            }

            return View(sizeModel);
        }

        // POST: Product/Size/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sizeModel = await _context.Sizes.FindAsync(id);
            _context.Sizes.Remove(sizeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeModelExists(int id)
        {
            return _context.Sizes.Any(e => e.Id == id);
        }
    }
}
