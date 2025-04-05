namespace CW2.DAL.Entities
{
    public class BookGenre
    {
        public required string BookId;
        public required int GenreId;

        public required Book Book;
        public required Genre Genre;
    }
}
