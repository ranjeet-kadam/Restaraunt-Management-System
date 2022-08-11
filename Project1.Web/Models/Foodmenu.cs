using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project1.Web.Models;

namespace Project1.Web.Models
{
    [Table(name: "FoodMenu")]
    public class Foodmenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int FoodId { get; set; }

        [Required]
        [StringLength(100)]

        public string FoodName { get; set; }
        public int FoodcategoryId { get; set; }
        [ForeignKey(nameof(Foodmenu.FoodcategoryId))]
        public FoodCategory FoodCategory { get; set; }

        [Required]
        [DefaultValue(1)]

        public short Quantity { get; set; }

        [Required]
        [DefaultValue(false)]

        public bool Confirmed { get; set; }

        public ICollection<Orders> Orders { get; set; }

    }
}
