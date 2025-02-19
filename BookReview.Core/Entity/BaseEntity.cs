namespace BookReview.Core.Entity
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            
        }

        public int Id { get; private set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; protected set; }
    }
}
