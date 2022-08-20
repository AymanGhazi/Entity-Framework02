using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class BlogImage
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Caption { get; set; }
    public int BlogForignKey { get; set; }
    [ForeignKey("BlogForignKey")]

    public virtual Blog Blog { get; set; }
}
