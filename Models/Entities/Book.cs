using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLibary.Models.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Photo { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
    }
}
