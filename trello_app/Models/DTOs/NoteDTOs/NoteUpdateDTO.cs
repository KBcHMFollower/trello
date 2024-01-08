namespace trello_app.Models.DTOs
{
    public class NoteUpdateDTO
    {
        public string? Name { get; set; }
        public int? sectionId { get; set; }
        public string? Description { get; set; }
    }
}
