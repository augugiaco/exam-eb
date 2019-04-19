using Exam.Application.Dtos;
using Exam.Application.Exceptions;
using Exam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exam.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var result = _userService.GetPaged(page, pageSize);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var userInfo =  await _userService.GetByIdAsync(id);

                return Ok(userInfo);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();

            }
            catch (System.Exception)
            {
                return StatusCode(500, "An error has ocurred when trying to process the request. Try again!");

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserDto userFromBody)
        {
            try
            {
                await _userService.UpdateAsync(userFromBody);

                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "An error has ocurred when trying to process the request. Try again!");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);

                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "An error has ocurred when trying to process the request. Try again!");

            }
        }

    }

}