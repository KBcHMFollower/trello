using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_app.Models
{
    public abstract class Entity<PK>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public PK Id { get; set; }
    }

    public abstract class Entity:Entity<int> 
    {
    }
}
