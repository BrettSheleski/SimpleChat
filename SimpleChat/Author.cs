using System;
using System.Collections.Generic;

namespace SimpleChat
{
    public class Author
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public virtual List<Message> Messages { get; set; }

        public AuthorImage Image { get; set; }
    }
}
