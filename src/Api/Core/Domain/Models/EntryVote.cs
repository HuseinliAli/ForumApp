using Common.ViewModels;

namespace Domain.Models
{
    public class EntryVote : BaseEntity
    {
        public VoteType VoteType { get; set; }
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public virtual Entry Entry { get; set; }
        public virtual User User { get; set; }
    }
}
