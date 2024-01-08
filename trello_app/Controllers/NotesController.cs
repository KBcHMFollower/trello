using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_app.Models.DTOs.SectionDTOs;
using trello_app.Models;
using static System.Collections.Specialized.BitVector32;
using Microsoft.EntityFrameworkCore;
using trello_app.Models.DTOs.NoteDTOs;
using trello_app.Models.DTOs;

namespace trello_app.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api")]
    public class NotesController : Controller
    {
        private readonly ApplicationContext _context;

        public NotesController(ApplicationContext context)
        {
            _context = context;
        }

        private object CreateOneResFromBoard(Note note)
        {
            return (new
            {
                note.Id,
                note.Name,
                note.Description,
                Section = note.Section.Id
            });
        }

        [HttpGet]
        [Route("boards/{boardId}/sections/{sectionId}/notes")]
        public async Task<IActionResult> GetAll(int boardId, int sectionId)
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

                BoardSection? boardSection = _context.BoardSections
                    .Include(sec=>sec.Notes)
                    .FirstOrDefault(sec => sec.Id == sectionId);

                if ( boardSection == null)
                {
                    return NotFound("Секция не найдена");
                }

                var notes = boardSection.Notes.Select(note => new
                {
                    note.Id,
                    note.Name,
                    note.Description,
                    Section = note.Section.Id
                }).ToList();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("boards/{boardId}/sections/{sectionId}/notes/{noteId}")]
        public async Task<IActionResult> GetOne(int boardId, int sectionId, int noteId)
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

                BoardSection? boardSection = _context.BoardSections
                    .Include(sec => sec.Notes)
                    .FirstOrDefault(sec => sec.Id == sectionId);

                if (boardSection == null)
                {
                    return NotFound("Секция не найдена");
                }

                Note? note = boardSection.Notes
                    .FirstOrDefault(note => note.Id == noteId);

                if (note == null)
                {
                    return NotFound("Note не найдена");
                }

                return Ok(CreateOneResFromBoard(note));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("boards/{boardId}/sections/{sectionId}/notes/{noteId}")]
        public async Task<IActionResult> Delete(int boardId, int sectionId, int noteId)
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

                BoardSection? boardSection = _context.BoardSections
                    .Include(sec => sec.Notes)
                    .FirstOrDefault(sec => sec.Id == sectionId);

                if (boardSection == null)
                {
                    return NotFound("Секция не найдена");
                }

                Note? note = boardSection.Notes
                    .FirstOrDefault(note => note.Id == noteId);

                if (note == null)
                {
                    return NotFound("Note не найдена");
                }

                var res = CreateOneResFromBoard(note);

                boardSection.Notes.Remove(note);
                _context.Notes.Remove(note);

                await _context.SaveChangesAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("boards/{boardId}/sections/{sectionId}/notes")]
        public async Task<IActionResult> Create([FromBody] NoteCreateDTO createInfo, int boardId, int sectionId)
        {
            try
            {
                if (createInfo.Name == null)
                {
                    return BadRequest("НЕ УКАЗАНО ИМЯ");
                }

                Board? board = _context.Boards
                   .Include(b => b.Sections)
                   .FirstOrDefault(b => b.Id == boardId);

                if (board == null)
                {
                    return NotFound("Доска не найдена");
                }

                BoardSection? boardSection = _context.BoardSections
                     .Include(sec => sec.Notes)
                     .FirstOrDefault(sec => sec.Id == sectionId);

                if (boardSection == null)
                {
                    return NotFound("Секция не найдена");
                }

                Note note = new Note 
                {
                    Name = createInfo.Name,
                    Section = boardSection,
                    Description = createInfo.Description,
                };

                boardSection.Notes.Add(note);

                await _context.SaveChangesAsync();

                return Ok(CreateOneResFromBoard(note));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("boards/{boardId}/sections/{sectionId}/notes/{noteId}")]
        public async Task<IActionResult> Update([FromBody] NoteUpdateDTO updateInfo, int boardId, int sectionId, int noteId)
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

                BoardSection? boardSection = _context.BoardSections
                     .Include(sec => sec.Notes)
                     .FirstOrDefault(sec => sec.Id == sectionId);

                if (boardSection == null)
                {
                    return NotFound("Секция не найдена");
                }

                Note? note = boardSection.Notes
                    .FirstOrDefault(note => note.Id == noteId);

                if (note == null)
                {
                    return NotFound("Note не найдена");
                }

                if (updateInfo.Name != null)
                {
                    note.Name = updateInfo.Name;
                }

                if (updateInfo.Description != null)
                {
                    note.Description = updateInfo.Description;
                }

                if (updateInfo.sectionId != null)
                {
                    BoardSection? newSection = board.Sections.Find(sec=>sec.Id == updateInfo.sectionId);
                    if (newSection == null)
                    {
                        return BadRequest("Нет секции на доске");
                    }

                    boardSection.Notes.Remove(note);
                    newSection.Notes.Add(note);
                }


                await _context.SaveChangesAsync();

                return Ok(CreateOneResFromBoard(note));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
