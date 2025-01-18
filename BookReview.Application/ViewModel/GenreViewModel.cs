namespace BookReview.Application.ViewModel
{
    public class GenreViewModel
    {
        public GenreViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}
