using System.Diagnostics.CodeAnalysis;
using NorthWindWebApi.DataAccessLayer;

namespace NorthwindWebApi.DataAccessLayer
{
    public class Supplier : BaseEntity
    {
        public Int32 SupplierId { set; get; }
        public String CompanyName { set; get; }
        public String ContactName { set; get; }
        public String ContactTitle { set; get; }
        public String Address { set; get; }
        public String City { set; get; }
        public String? Region { set; get; }
        public String PostalCode { set; get; }
        public String Country { set; get; }
        public String Phone { set; get; }
        public String? Fax { set; get; }
        public String? HomePage { set; get; }
        public ICollection<Product>? Products { set; get; }
    }
}
