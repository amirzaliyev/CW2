using CW2.DAL.Entities;

namespace CW2.Models
{
    public class BookGenreViewModel
    {
        public required string BookId;
        public required int GenreId;

        public required Book Book;
        public required Genre Genre;
    }
}
