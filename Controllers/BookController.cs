using Microsoft.AspNetCore.Mvc;
using WebLibary.Models.Entities;
using WebLibary.Repository.ERepository;

namespace WebLibary.Controllers
{
    public class BookController : Controller
    {
        private readonly TRepository<Book> _bookRepository;

        public BookController(TRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAll();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.Create(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookRepository.Update(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
