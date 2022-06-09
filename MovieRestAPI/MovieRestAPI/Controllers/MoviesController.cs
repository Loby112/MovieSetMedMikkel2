using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLib;
using MovieRestAPI.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRestAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase{
        private MovieManagers manager = new MovieManagers();
        // GET: api/<MoviesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery] int? filter){
            var result = manager.GetMovies(filter);
            if (!result.Any()){
                return NoContent();
            }
            return Ok(result);
        }

        // GET api/<MoviesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id){
            var result = manager.GetById(id);
            if (result == null){
                return NotFound("No object found with this Id");
            }
            return Ok(result);
        }

        // POST api/<MoviesController>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<Movie> Post([FromBody] Movie value){
            if (value.LengthInMinutes <= 0){
                return BadRequest("Length must be more than 0");
            }

            manager.AddMovie(value);
            return Created("api/Movies/" + value.Id, value);
        }

        // PUT api/<MoviesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Movie> Put(int id, [FromBody] Movie value){
            var result = manager.GetById(id);
            if (result == null){
                return NotFound("No object with this id");
            }

            if (value.LengthInMinutes <= 0){
                return BadRequest("Length must be more than 0");
            }
            return Ok(manager.UpdateMovie(id, value));
        }

        // DELETE api/<MoviesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Movie> Delete(int id){
            var result = manager.GetById(id);
            if (result == null){
                return NotFound("Object with Id wasn't found");
            }
            return Ok(manager.DeleteMovie(id));
        }
    }
}
