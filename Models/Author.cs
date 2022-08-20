using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class Author
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string FirtsName { get; set; }
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    [MaxLength(150)]

    public string DisplayName { get; set; }
}
