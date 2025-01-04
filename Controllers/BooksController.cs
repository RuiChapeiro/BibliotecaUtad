using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUtad.Data;
using BibliotecaUtad.Models;

namespace BibliotecaUtad.Controllers
{
    public class BooksController : Controller
    {
        private readonly BibliotecaUtadContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(BibliotecaUtadContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bibliotecaUtadContext = _context.Book.Include(b => b.Gender).Include(b => b.Subgender);
            return View(await bibliotecaUtadContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Gender)
                .Include(b => b.Subgender)
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName");
            ViewData["SubGenderId"] = new SelectList(_context.Set<Subgender>(), "SubGenderId", "SubGenderName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ISBN,Title,Author,Editor,N_Copies,CoverImage,Summary,LaunchDate,GenderId,SubGenderId")] BookViewModel book)
        {
            //Verifica as extensões permitidas
            var CoverExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            var extension = Path.GetExtension(book.CoverImage.FileName).ToLower();
            if (!CoverExtensions.Contains(extension))
            {
                ModelState.AddModelError("CoverImage", "A imagem de capa deve ser um arquivo .jpg, .jpeg ou .png.");
            }


            if (ModelState.IsValid)
            {
                var newBook = new Book(); //Criar um novo livro e popular com os dados selecionados
                newBook.ISBN = book.ISBN;
                newBook.Title = book.Title;
                newBook.Author = book.Author;
                newBook.Editor = book.Editor;
                newBook.N_Copies = book.N_Copies;
                newBook.CoverImage = book.ISBN + extension; // Mudar o nome da imagem para ser igual ao ISBN
                newBook.Summary = book.Summary;
                newBook.LaunchDate = book.LaunchDate;
                newBook.GenderId = book.GenderId;
                newBook.SubGenderId = book.SubGenderId;
                newBook.AquisitionDate = DateTime.Now; // Define a data de aquisição como a data atual

                // Guardar a imagem de capa no diretório certo
                string coverFileName = book.ISBN + extension; // Mudar o nome da imagem para ser igual ao ISBN
                string coverPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "covers", coverFileName);

                using (var fileStream = new FileStream(coverPath, FileMode.Create))
                {
                    await book.CoverImage.CopyToAsync(fileStream);
                }

                _context.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName", book.GenderId);
            ViewData["SubGenderId"] = new SelectList(_context.Set<Subgender>(), "SubGenderId", "SubGenderName", book.SubGenderId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName", book.GenderId);
            ViewData["SubGenderId"] = new SelectList(_context.Set<Subgender>(), "SubGenderId", "SubGenderName", book.SubGenderId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ISBN,Title,Author,Editor,N_Copies,CoverImage,Summary,LaunchDate,GenderId,SubGenderId")] Book book)
        {
            if (id != book.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ISBN))
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
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName", book.GenderId);
            ViewData["SubGenderId"] = new SelectList(_context.Set<Subgender>(), "SubGenderId", "SubGenderName", book.SubGenderId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Gender)
                .Include(b => b.Subgender)
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.ISBN == id);
        }

        // Método para obter subgêneros com base no gênero selecionado
        public JsonResult GetSubgenders(int genderId)
        {
            var subgenders = _context.Subgender
                                     .Where(s => s.GenderId == genderId)
                                     .ToList();
            return Json(subgenders);
        }
    }
}