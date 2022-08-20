using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; } // Navigation Properity
    public virtual ICollection<Tag> Tags { get; set; }
    public virtual ICollection<PostTags> PostTags { get; set; }
    public bool IsDeleted { get; set; }

}

public class Tag
{
    public string tagId { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<PostTags> PostTags { get; set; }

}
public class PostTags
{
    public int PostId { get; set; }
    public virtual Post Post { get; set; }
    public string TagId { get; set; }
    public virtual Tag Tag { get; set; }
    public DateTime AddedOn { get; set; }

}
