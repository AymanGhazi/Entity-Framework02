using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Models;

//[Index(nameof(Url), IsUnique = true, Name = "URL_Index")]
public class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public DateTime CreatedOn { get; set; }
    public virtual List<Post> Posts { get; set; } // Domain Model as it is Navigation Properity
    public int rating { get; set; }
    public virtual BlogImage BlogImage { get; set; }

}
