using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var page = _context.Categories.ToList();
            return View(page);
        }

        public IActionResult Create()
        {

            return View();
         }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id) {
            var delete = _context.Categories.Find(Id);
            if (delete == null) return NotFound();
            if (delete!=null)
            {

            _context.Categories.Remove(delete);
            _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id) {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category) {
            var exitcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (exitcategory == null) return NotFound();
            exitcategory.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
    }
}
