using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_app.Models
{
    public class Board:Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<BoardSection> Sections { get; set; } = new List<BoardSection>();
    }
}
