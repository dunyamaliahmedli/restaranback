using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MenyuController : Controller
    {
        private IWebHostEnvironment _env { get; }
        private AppDbContext _context { get; }
        public MenyuController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create() {

            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Create(Menyu menyu)
        {
            if (!ModelState.IsValid) return View(menyu);




            string filename = menyu.Photo.FileName;
            if (filename.Length>64)
            {
                filename.Substring(filename.Length - 64, 64);
            }
            string newfilename = Guid.NewGuid().ToString() + filename;
            string path = Path.Combine(_env.WebRootPath, "assets", "img", newfilename);
            using (FileStream fs = new FileStream(path,FileMode.Create))
            {
                menyu.Photo.CopyTo(fs);
            }
            menyu.Image = newfilename;
                await _context.Menyus.AddAsync(menyu);
            await _context.SaveChangesAsync();

            return View(nameof(Index));
        }
    }
}
