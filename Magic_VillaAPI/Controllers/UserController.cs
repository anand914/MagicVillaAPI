using Magic_VillaAPI.Models;
using Magic_VillaAPI.Models.Dto;
using Magic_VillaAPI.Repository.IRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_VillaAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private ApiResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("UserName and Password not Matche");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }
        
        [HttpPost("RegisterUser")]
        public async Task<IActionResult>Registration([FromBody]RegisterRequestDTO model)
        {
            try
            {
                var isUniqueUser = _userRepository.IsUniqueUser(model.UserName);
                if(!isUniqueUser)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("UserName already Exist");
                    return BadRequest(_response);
                }
                var user = await _userRepository.Register(model);
                if(user==null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Error While Registering");
                    return BadRequest(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = "User Register succesfully";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
    }
}
