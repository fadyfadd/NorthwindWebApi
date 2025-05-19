using System.ComponentModel.DataAnnotations.Schema;

namespace NorthWindWebApi.DataAccessLayer;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int32 Id { set; get; }
}