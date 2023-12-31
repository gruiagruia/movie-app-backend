﻿using Auth;
using Dtos;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace movie_app_backend.Controllers;

[Route("/api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, 
        IAuthService authService, ILogger<UserController> logger)
    {
        _userService = userService;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("users/signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
    {
        var createdUser = await _authService.SignUp(signUpModel);
        if (createdUser == null)
        {
            _logger.LogWarning($"Could not create user : {signUpModel.Username}.");
            return BadRequest("User could not be created.");
        }
        return Created("User created successfuly",createdUser);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Could not retrieve user with id {userId}. Service returned null.");
            return BadRequest("Could not retrieve user with passed id.");
        }

        return Ok(user.ToUserDto());
    }

    [HttpPut("users/{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDto userToUpdate)
    {
        var updatedUser = await _userService.UpdateUserAsync(userToUpdate);

        if(updatedUser == null)
        {
            _logger.LogWarning($"Could not update user {userToUpdate.Username}. Service returned null.");
            return BadRequest("Could not update user with passed id.");
        }

        return Ok(updatedUser);
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await _userService.DeleteUserAsync(userId);

        if (!result)
        {
            _logger.LogWarning($"Could not delete user {userId}. Service returned null.");
            return BadRequest("Could not delete user with passed id.");
        }

        return Ok("User deleted successfully!");
    }
}

