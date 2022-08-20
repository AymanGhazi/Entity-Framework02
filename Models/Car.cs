using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Framework.Models;

public class Car
{
    public int CarId { get; set; }
    public string State { get; set; }
    public string LicencePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public virtual List<RecordOfSale> SaleHsitory { get; set; }
}
public class RecordOfSale
{
    public int RecordOfSaleId { get; set; }
    public string CarState { get; set; }
    public DateTime DateSold { get; set; }
    public decimal price { get; set; }
    public string CarLicencePlate { get; set; }
    public virtual Car Car { get; set; }
}
