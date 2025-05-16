using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Users.Domain
{
    public class Branch : Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
