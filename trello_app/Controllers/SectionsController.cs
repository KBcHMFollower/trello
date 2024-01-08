using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trello_app.Models;
using trello_app.Models.DTOs.BoardDTOs;
using trello_app.Models.DTOs.SectionDTOs;

namespace trello_app.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api")]
    public class SectionsController : Controller
    {
        private readonly ApplicationContext _context;

        public SectionsController (ApplicationContext context)
        {
            _context = context;
        }

        private object CreateOneResFromBoard(BoardSection sec)
        {
            return (new
            {
                sec.Id,
                sec.Name,
                sec.Description,
                Board = sec.Board.Id
            });
        }

        [HttpGet]
        [Route("boards/{boardId}/sections")]
        public async Task<IActionResult> GetAll(int boardId)
        {
            try
            {
                Board? board = _context.Boards
                    .Include(b=>b.Sections)
                    .FirstOrDefault(b=>b.Id == boardId);
               
                if (board == null)
                {
                    return NotFound("Доска не найдена");
                }

                var sections = board.Sections.Select(section => new
                {
                    section.Id,
                    section.Name,
                    section.Description,
                    board = board.Id 
                }).ToList();

                return Ok(sections);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("boards/{boardId}/sections/{sectionId}")]
        public async Task<IActionResult> GetOne(int boardId, int sectionId)
        {
            try
            {
                Board? board = _context.Boards
                    .Include(b => b.Sections)
                    .FirstOrDefault(b => b.Id == boardId);

                if (board == null)
                {
                    return NotFound("Доска не найдена");
                }


                BoardSection? section = board.Sections
                    .FirstOrDefault(sec => sec.Id == sectionId);

                if (section == null)
                {
                    return NotFound("Секция не найдена");
                }

                return Ok(CreateOneResFromBoard(section));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("boards/{boardId}/sections/{sectionId}")]
        public async Task<IActionResult> Delete(int boardId, int sectionId)
        {
            try
            {
                Board? board = _context.Boards
                    .Include(b=>b.Sections)
                    .FirstOrDefault(b=>b.Id==boardId);

                if (board == null)
                {
                    return NotFound("Доска не найдена");
                }

                BoardSection? section = board.Sections
                    .FirstOrDefault(sec=>sec.Id == sectionId);

                if (section == null)
                {
                    return NotFound("Секция не найдена");
                }

                board.Sections.Remove(section);
                _context.BoardSections.Remove(section);

                await _context.SaveChangesAsync();

                return Ok(section);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("boards/{boardId}/sections")]
        public async Task<IActionResult> Create(int boardId, [FromBody] SectionCreateDTO createInfo )
        {
            try
            {
                if (createInfo.Name == null)
                {
                    return BadRequest("Invalid data");
                }

                Board? ownerBoard = _context.Boards
                    .Include(b => b.Sections)
                    .FirstOrDefault(b => b.Id == boardId);

                if (ownerBoard == null)
                {
                    return NotFound("Доска не найден");
                }

                BoardSection newSection = new BoardSection
                {
                    Name = createInfo.Name,
                    Description = "",
                    Board = ownerBoard
                };
                ownerBoard.Sections.Add(newSection);

                await _context.SaveChangesAsync();

                return Ok(CreateOneResFromBoard(newSection));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("boards/{boardId}/sections/{sectionId}")]
        public async Task<IActionResult> Update([FromBody] SectionUpdateDTO updateInfo, int boardId, int sectionId)
        {
            try
            {
                if (updateInfo == null)
                {
                    return BadRequest("нет данных для update");
                }

                Board? board = _context.Boards
                    .Include(b => b.Sections)
                    .FirstOrDefault(b => b.Id == boardId);

                if (board == null)
                {
                    return NotFound("Доска не найдена");
                }

                BoardSection? section = board.Sections
                    .FirstOrDefault(sec => sec.Id == sectionId);

                if (section == null)
                {
                    return NotFound("Секция не найдена");
                }
              
                if (updateInfo.Name != null)
                {
                    section.Name = updateInfo.Name;
                }

                if (updateInfo.Description != null)
                {
                    section.Description = updateInfo.Description;
                }

                await _context.SaveChangesAsync();

                return Ok(CreateOneResFromBoard(section));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
