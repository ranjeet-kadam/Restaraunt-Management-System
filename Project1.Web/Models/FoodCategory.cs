using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project1.Web.Models;

namespace Project1.Web.Models
{
    [Table(name: "FoodCategory")]
    public class FoodCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int FoodcategoryId { get; set; }



        [Required]
        [StringLength(100)]

        public string FoodcategoryName { get; set; }

        public ICollection<Foodmenu> foodmenus { get; set; }

    }
}
