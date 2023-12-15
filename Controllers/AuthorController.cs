using Microsoft.AspNetCore.Mvc;
using WebLibary.Models.Entities;
using WebLibary.Repository.ERepository;

namespace WebLibary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly TRepository<Author> _authorRepository;

        public AuthorController(TRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAll();
            return View(authors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);

        }
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepository.Create(author);
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _authorRepository.Update(author);
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
