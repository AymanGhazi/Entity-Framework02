using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class Book
{
    [Key]
    public int BookKey { get; set; }
    public String Name { get; set; }
    public String Author { get; set; }

}
