using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class AuditEntry
{
    public int Id { get; set; }
    public string userName { get; set; }
    public string Action { get; set; }

}
