using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_app.Models
{
    public class User:Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Pass {  get; set; }
        [Required]
        public string PassSalt { get; set; }
        public List<Board> Boards { get; set; } = new List<Board>();
    }
}
