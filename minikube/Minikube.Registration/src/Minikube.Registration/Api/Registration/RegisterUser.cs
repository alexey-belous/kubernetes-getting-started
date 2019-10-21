using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Minikube.Registration.Api.Registration
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisteredUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public interface IUserRegisterer
    {
        Task<Maybe<RegisteredUserResponse>> Register(RegisterUserRequest user);
    }

    public class UserRegisterer : IUserRegisterer
    {
        private readonly IUserWriter _writer;
        private readonly IUserFinder _finder;

        public UserRegisterer(
            IUserWriter writer,
            IUserFinder finder)
        {
            _writer = writer;
            _finder = finder;
        }

        public async Task<Maybe<RegisteredUserResponse>> Register(RegisterUserRequest user)
        {
            var isUserExist = await _finder.FindByEmail(user.Email);
            if (isUserExist.HasError)
            {
                var createdUser = await _writer.SaveAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Email = user.Email,
                });
                return new Maybe<RegisteredUserResponse>(new RegisteredUserResponse
                {
                    Id = createdUser.Id,
                    Email = createdUser.Email,
                });
            }

            return new Maybe<RegisteredUserResponse>("User with specified email already exists.");
        }
    }

    [ApiController]
    [Route("api/v1")]
    public class RegisterUser : ControllerBase
    {
        private readonly IUserRegisterer _userRegisterer;
        public RegisterUser(IUserRegisterer userRegisterer)
        {
            _userRegisterer = userRegisterer;
        }

        [HttpPost("user")]
        public async Task<ActionResult> Register([FromBody]RegisterUserRequest request)
        {
            var res = await _userRegisterer.Register(request);

            if (!res.HasError)
            {
                return Ok(res.Result);
            }

            return BadRequest(new ApiError(res.Error));
        }
    }
}