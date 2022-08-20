using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Models;


[Index(nameof(FirstName), nameof(lastName))]
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string lastName { get; set; }

}
