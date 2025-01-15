namespace BookReview.Core.Entity
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Active = true;            
            Reviews = new List<Review>();
        }
       
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; set; }
        public List<Review> Reviews { get; private set; }

        public void UpdateStatus(bool active)
        {
            Active = active;
        }

        public void Update(string fullName, string email)
        {
            if(!string.IsNullOrEmpty(fullName))
                FullName = fullName;

            if (!string.IsNullOrEmpty(email))
                Email = email;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
        }
    }
}
