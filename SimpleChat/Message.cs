using System;

namespace SimpleChat
{
    public class Message
    {
        public virtual int Id { get; set; }
        public virtual int TopicId { get; set; }
        public virtual Guid AuthorId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Text { get; set; }

        public virtual Author Author { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
