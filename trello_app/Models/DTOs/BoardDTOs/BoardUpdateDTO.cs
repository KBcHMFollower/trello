namespace trello_app.Models.DTOs.BoardDTOs
{
    public class BoardUpdateDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? deleteId { get; set; }
        public int? addId { get; set; }
    }
}
