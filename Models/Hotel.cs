using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Hotel
    {
        [Key]
        public int id {  get; set; }
        [Required]
        [StringLength(35)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(35)]
        public string City {  get; set; }
        [Required]
        [StringLength (50)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Phone { get; set; }   
        [Required]
        [StringLength(100)]
        public string Images {  get; set; } 
    
    }
}
