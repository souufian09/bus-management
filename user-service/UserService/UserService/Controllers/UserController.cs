using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);

                if (user == null)
                    return NotFound(new
                    {
                        message = "Utilisateur non trouvé"
                    });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("chauffeurs")]
        public async Task<IActionResult> GetChauffeurs()
        {
            try
            {
                var chauffeurs = await _userService
                    .GetChauffeurs();
                return Ok(chauffeurs);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("controleurs")]
        public async Task<IActionResult> GetControleurs()
        {
            try
            {
                var controleurs = await _userService
                    .GetControleurs();
                return Ok(controleurs);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPost("users")]
        public async Task<IActionResult> Create(
            CreateUserDto dto)
        {
            try
            {
                var user = await _userService.Create(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPut("users/{id}")]
        public async Task<IActionResult> Update(
            int id, UpdateUserDto dto)
        {
            try
            {
                var user = await _userService
                    .Update(id, dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userService.Delete(id);
                return Ok(new
                {
                    message = "Utilisateur supprimé"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
