using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Products
{
    public class Product : ProductBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //  [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }


    }
}
