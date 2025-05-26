using System.ComponentModel.DataAnnotations.Schema;
using NorthwindWebApi.DataAccessLayer;

namespace NorthWindWebApi.DataAccessLayer;

public class Product : BaseEntity
{
   
    public Int32? ProductId { set; get; }
    public String ProductName { set; get; }
    public Int32? SupplierId { set; get; }
    public Int32? CategoryId { set; get; }
    public String QuantityPerUnit { set; get; }
    public Double? UnitPrice { set; get; }
    public Int32? UnitsInStock { set; get; }
    public Int32? UnitsOnOrder { set; get; }
    public Int32? ReorderLevel { set; get; }
    public Int32 Discontinued { set; get; }
    public Supplier Supplier { set; get; }
    
}