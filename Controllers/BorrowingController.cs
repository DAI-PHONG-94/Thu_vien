using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLibary.Models.Entities;
using WebLibary.Repository.ERepository;
using WebLibary.Repository;

namespace WebLibary.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly TRepository<Borrowing> _borrowingRepository;
        private readonly TRepository<Book> _bookRepository;
        private readonly IUserRepository _userRepository;

        public BorrowingController(TRepository<Borrowing> borrowingRepository, TRepository<Book> bookRepository, IUserRepository userRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var borrowings = await _borrowingRepository.GetAll();
            return View(borrowings);
        }

        public IActionResult Create()
        {
            ViewBag.Books = _bookRepository.GetAll();
            ViewBag.Users = _userRepository.GetAllUsersAsync();
            ViewBag.BorrowDate = DateTime.Now;
            ViewBag.ReturnDate = DateTime.Now.AddDays(7); // Default return date is 7 days from now
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, int userId, DateTime borrowDate, DateTime returnDate)
        {
            if (ModelState.IsValid)
            {
                var borrowing = new Borrowing
                {
                    BookId = bookId,
                    UserId = userId,
                    BorrowDate = borrowDate,
                    ReturnDate = returnDate
                };
                await _borrowingRepository.Create(borrowing);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Books = _bookRepository.GetAll();
            ViewBag.Users = _userRepository.GetAllUsersAsync();

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var borrowing = await _borrowingRepository.GetById(id);
            return borrowing == null ? NotFound() : View(borrowing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Borrowing borrowing)
        {
            if (id != borrowing.BorrowingId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _borrowingRepository.Update(borrowing);
                return RedirectToAction(nameof(Index));
            }

            return View(borrowing);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var borrowing = await _borrowingRepository.GetById(id);
            return borrowing == null ? NotFound() : View(borrowing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _borrowingRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
