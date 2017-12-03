using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guardian.Website.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public User CreatedByUser { get; set; }
        public User ModifiedByUser { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int TagID { get; set; }
        public string Text { get; set; }
        public TagType Type { get; set; }

        public User CreatedByUser { get; set; }
        public User ModifiedByUser { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }

    }

    public class TagType
    {
        public int TagTypeID { get; set; }
        public string Description { get; set; }

        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string EMailAddress { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}