using BookReview.Core.Enums;

namespace BookReview.Core.Entity
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Role = UserRoleEnum.User;

            Active = true;            
            Reviews = new List<Review>();            
        }
       
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; set; }

        public string? TemporaryPassword { get; private set; }
        public DateTime? ValidateHash { get; private set; }
        public UserRoleEnum Role { get; private set; }

        public List<Review> Reviews { get; private set; }

        public void UpdateStatus(bool active)
        {
            Active = active;
            UpdateAt = DateTime.UtcNow;
        }

        public void Update(string fullName, string email, bool? status)
        {
            if(!string.IsNullOrEmpty(fullName))
                FullName = fullName;

            if (!string.IsNullOrEmpty(email))
                Email = email;

            if(status != null)
                Active = (bool)status;

            UpdateAt = DateTime.UtcNow;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
            TemporaryPassword = null;
            ValidateHash = null;
        }

        public void StartRecoveryPassword(string temporaryPassword)
        {
            TemporaryPassword = temporaryPassword;
            ValidateHash = DateTime.Now.AddHours(1);
        }
    }
}
