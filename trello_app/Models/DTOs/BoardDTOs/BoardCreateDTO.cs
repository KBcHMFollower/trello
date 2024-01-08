namespace trello_app.Models.DTOs.BoardDTOs
{
    public class BoardCreateDTO
    {
        public string Name { get; set; }
        public int userId { get; set; }
        public string description { get; set; }
    }
}
