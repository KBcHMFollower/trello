using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Text.Json;
using trello_app.Models;
using trello_app.Models.DTOs.BoardDTOs;
using trello_app.Services;

namespace trello_app.Controllers
{
    [ApiController]
    [Route("api/boards")]
    //[Authorize]
    public class BoardsController : Controller
    {
        private readonly ApplicationContext _context;

        public BoardsController(ApplicationContext context)
        {
            _context = context;
        }

        private object CreateOneResFromBoard(Board board)
        {
            var users = board.Users.Select(user => new
            {
                user.Id,
                user.Name,
                user.Email
            });

            return (new
            {
                Name = board.Name,
                Description = board.Description,
                Id = board.Id,
                Users = users.ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? userId)
        {
            try
            {
                IQueryable<Board> query = _context.Boards;

                if (userId.HasValue)
                {
                    query = query.Where(board => board.Users.Any(user => user.Id == userId));
                }

                var boards = query.Include(b => b.Users).ToList();

                var boardsDTO = boards.Select(boards => new
                {
                    boards.Id,
                    boards.Name,
                    boards.Description,
                    Users = boards.Users.Select(user => new
                    {
                        user.Id,
                        user.Name,
                        user.Email
                    }).ToList(),
                }).ToList();

                return Ok(boardsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                Board? board = _context.Boards
                     .Include(b => b.Users).
                     FirstOrDefault(b => b.Id == id);

                if (board == null)
                {
                    return BadRequest("Доска с заданным id не нйден");
                }
                return Ok(CreateOneResFromBoard(board));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BoardCreateDTO boardInfo)
        {
            try
            {
                if (boardInfo == null)
                {
                    return BadRequest("Invalid board data");
                }

                User? owner = _context.Users.Find(boardInfo.userId);
                if (owner == null)
                {
                    return BadRequest("Пользователь не найден");
                }

                Board newBoard = new Board
                {
                    Name = boardInfo.Name,
                    Description = boardInfo.description
                };
                newBoard.Users.Add(owner);


                var createdBoard = _context.Boards.Add(newBoard).Entity;
                await _context.SaveChangesAsync();

                return Ok(CreateOneResFromBoard(createdBoard));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] BoardUpdateDTO updateInfo, int id)
        {
            try
            {
                if (updateInfo == null)
                {
                    return BadRequest("нет данных для update");
                }
                
                Board? board = _context.Boards
                    .Include(b=>b.Users).
                    FirstOrDefault(b=>b.Id == id);

                if (board == null)
                {
                    return BadRequest("Доска не найдена");
                }

                if (updateInfo.Name != null)
                {
                    board.Name = updateInfo.Name;
                }

                if (updateInfo.Description != null)
                {
                    board.Description = updateInfo.Description;
                }

                if (updateInfo.addId != null)
                {
                    User? user = _context.Users.Find(updateInfo.addId);
                    if(user == null)
                    {
                        return BadRequest("Пользователь не найден");
                    }

                    bool hasUser = board.Users.Any(user => user.Id == updateInfo.addId);
                    if (hasUser)
                    {
                        return BadRequest("Пользователь уже добавлен");
                    }

                    board.Users.Add(user);
                }

                if (updateInfo.deleteId != null)
                {
                    User? user = _context.Users.Find(updateInfo.deleteId);
                    if (user == null)
                    {
                        return BadRequest("Пользователь не найден");
                    }

                    bool hasUser = board.Users.Any(user => user.Id == updateInfo.deleteId);
                    if (!hasUser)
                    {
                        return BadRequest("Пользователя нет на доске");
                    }

                    board.Users.Remove(user);
                }


                await _context.SaveChangesAsync();



                return Ok(CreateOneResFromBoard(board));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            try
            {
                Board? board = _context.Boards.Find(id);
                if (board == null)
                {
                    return BadRequest("Доска не найдена");
                }

                _context.Boards.Remove(board);

                await _context.SaveChangesAsync();

                return Ok(CreateOneResFromBoard(board));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
