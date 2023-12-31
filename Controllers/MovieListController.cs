using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using movie_app_backend.Exceptions;

namespace movie_app_backend.Controllers;

[Route("/api/")]
[ApiController]
public class MovieListController: ControllerBase
{
    private readonly IMovieListService _movieListService;

    public MovieListController(IMovieListService movieListService)
    {
        _movieListService = movieListService;
    }

    [HttpGet("movielist/{profileId}")]
    public async Task<ActionResult<ICollection<MovieList>>> GetMovieListByProfileId(string profileId)
    {
        try
        {
            var movieList = await _movieListService.GetMovieListByProfileIdAsync(profileId);
            return Ok(movieList);
        }
        catch (Exception e)
        {
            return e is ArgumentException ? BadRequest(e.Message) : StatusCode(500, e.Message);
        }
    }

    [HttpPost("movielist")]
    public async Task<ActionResult<MovieList>> CreateMovieList([FromBody] MovieList movieList)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdList = await _movieListService.CreateMovieListAsync(movieList);
            return Created($"/MovieList/{createdList.MovieListId}", createdList);
        }
        catch (Exception e)
        {
            return e is ArgumentException ? BadRequest(e.Message) : StatusCode(500, e.Message);
        }
    }

    [HttpPut("movielist")]
    public async Task<ActionResult<MovieList>> UpdateMovieList([FromBody] MovieList movieList)
    {
        try
        {
            var updated = await _movieListService.UpdateMovieListAsync(movieList);
            return Ok(updated);
        }
        catch (Exception e)
        {
            if (e is NotFoundException)
            {
                return NotFound();
            }

            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("movielist")]
    public async Task<IActionResult> DeleteMovieList(string movieListId)
    {
        try
        {
            await _movieListService.DeleteMovieListAsync(movieListId);
            return Ok("MovieList " + movieListId + " deleted");
        }
        catch (Exception e)
        {
            if (e is NotFoundException)
            {
                return NotFound();
            }

            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("movielist/{movieListId}/movies/{imbdId}")]
    public async Task<IActionResult> AddMovieToMovieList(string movieListId, string imbdId)
    {
        try
        {
            await _movieListService.AddMovieToMovieListAsync(movieListId, imbdId);
            return Ok("Movie " + imbdId + " added to MovieList.");
        }
        catch (Exception e)
        {
            return e is ArgumentException ? BadRequest(e.Message) : StatusCode(500, e.Message);
        }
    }

    [HttpDelete("movielist/{movieListId}/movies/{imbdId}")]
    public async Task<IActionResult> RemoveMovieFromMovieList(string movieListId, string imbdId)
    {
        try
        {
            await _movieListService.RemoveMovieFromMovieListAsync(movieListId, imbdId);
            return Ok("Movie " + imbdId + " removed from MovieList.");
        }
        catch (Exception e)
        {
            if (e is NotFoundException)
            {
                return NotFound();
            }

            return StatusCode(500, e.Message);
        }
    }
}