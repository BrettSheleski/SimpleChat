using System.Collections.Generic;

namespace SimpleChat
{
    public class Topic
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
