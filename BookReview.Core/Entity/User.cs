namespace BookReview.Core.Entity
{
    public class User : BaseEntity
    {
        public User(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            Active = true;
            Reviews = new List<Review>();
        }
       
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; set; }
        public List<Review> Reviews { get; private set; }
    }
}
