using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Data;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _context.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookFromDb = await _context.Books.FindAsync(Book.Id);
                if (bookFromDb is not null)
                {
                    bookFromDb.Name = Book.Name;
                    bookFromDb.Author = Book.Author;
                    bookFromDb.ISBN = Book.ISBN;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
                return Page();
            }

            return Page();
        }
    }
}
