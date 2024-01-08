using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_app.Models
{
    public class BoardSection:Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        [Required]
        [ForeignKey("Board_id")]
        public Board Board { get; set; }
    }
}
