using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChat
{
    public class AuthorImage
    {
        public virtual Guid Id { get; set; }

        public virtual string Filename { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual string ContentType { get; set; }
        public virtual Author Author { get; set; }
    }
}
