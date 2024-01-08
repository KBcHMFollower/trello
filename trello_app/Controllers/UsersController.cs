using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using trello_app.Models;
using trello_app.Models.DTOs;
using trello_app.Services;

namespace trello_app.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public UsersController(ApplicationContext context, AuthOptions authOptions)
        {
            _context = context;
        }

        private object CreateOneResFromBoard(User user)
        {
            var boards = user.Boards.Select(board => new
            {
                board.Id,
                board.Name,
            });

            return (new
            {
                user.Id,
                user.Name,
                user.Email,
                Boards = boards.ToList()
            });
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] string? email)
        {
            try
            {
                IQueryable<User> query = _context.Users;

                    query = query.Where(x => x.Email.Contains(email));

                var users = query.Include(u => u.Boards).ToList();

                var usersDTO = users.Select(user => new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    Boards = user.Boards.Select(board => new
                    {
                        board.Id,
                        board.Name
                    }).ToList(),
                }).ToList();

                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                User? user = _context.Users
                    .Include(u=>u.Boards)
                    .FirstOrDefault(u=>u.Id == id);
                if (user == null)
                {
                    return BadRequest("Пользователь с заданным id не нйден");
                }
                return Ok(CreateOneResFromBoard(user));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user data");
                }

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    return BadRequest("Пользователь с таким email уже существует");
                }

                string passSalt = PassHasher.GenerateSalt();
                string hashPass = PassHasher.HashPassword(user.Pass, passSalt);
                var createdUser = _context.Users.Add(new Models.User
                {
                    Name = user.Name,
                    Pass = hashPass,
                    PassSalt = passSalt,
                    Email = user.Email
                }
                ).Entity;
                await _context.SaveChangesAsync();

                var jwt = JwtCreator.CreateJwt(createdUser.Email, createdUser.Id.ToString(), createdUser.Name);

                return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO logDTO)
        {
            try
            {
                if (logDTO == null || string.IsNullOrEmpty(logDTO.Email) || string.IsNullOrEmpty(logDTO.Pass))
                {
                    return BadRequest("Invalid login data");
                }

                User? user = _context.Users.SingleOrDefault(u => u.Email == logDTO.Email);
                if (user == null)
                {
                    return NotFound("Пользователь с указанным адресом электронной почты не найден");
                }

                if (!PassHasher.VerifyPassword(logDTO.Pass, user.Pass, user.PassSalt))
                {
                    return BadRequest("Неверный пароль");
                }

                var jwt = JwtCreator.CreateJwt(user.Email, user.Id.ToString(), user.Name);

                return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("auth")]
        [Authorize]
        public async Task<IActionResult> CheckAuth()
        {
            try
            {
                string? email = User.FindFirst(ClaimTypes.Email)?.Value;
                string? id = User.FindFirst("id")?.Value;
                string? name = User.FindFirst(ClaimTypes.Name)?.Value;
                var jwt = JwtCreator.CreateJwt(email, id, name);
                return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
