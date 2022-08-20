using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class Category
{
    public byte Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
}
