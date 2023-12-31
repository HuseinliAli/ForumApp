using Common.ViewModels;

namespace Domain.Models
{
    public class EntryCommentVote : BaseEntity
    {
        public VoteType VoteType { get; set; }
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public virtual EntryComment EntryComment { get; set; }
        public virtual User User { get; set; }
    }
}
