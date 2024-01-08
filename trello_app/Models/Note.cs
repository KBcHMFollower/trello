using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_app.Models
{
    public class Note:Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [ForeignKey("BoardSection_id")]
        public BoardSection Section { get; set; }
    }
}
