using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class Order
{
    public int Id { get; set; }
    public long OrderNo { get; set; }
    public double Amount { get; set; }
}
public class OrderTest
{
    public int Id { get; set; }
    public long OrderNo { get; set; }
    public double Amount { get; set; }
}
