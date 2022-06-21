using ApiTask1.DAL;
using ApiTask1.Dtos;
using ApiTask1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        readonly AppDbContext _context;

        public ActorController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Actors.ToListAsync());
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int id)
        {
            Actor actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null) return BadRequest();

            return Ok(actor);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(ActorCreateDto actorDto)
        {
            Actor actor = new Actor
            {
                FullName = actorDto.FullName,
                ImageUrl = actorDto.ImageUrl,
                IsDeleted = false
            };

           await _context.Actors.AddAsync(actor);
           await _context.SaveChangesAsync();

            return Ok(actorDto);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, ActorUpdateDto actorDto)
        {
            if (actorDto == null) return StatusCode(StatusCodes.Status400BadRequest);
            Actor actor = await _context.Actors.FindAsync(id);
            if (actor == null) return StatusCode(StatusCodes.Status404NotFound);
            actor.FullName = actorDto.FullName;
            actor.ImageUrl = actorDto.ImageUrl;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Actor actor = await _context.Actors.FindAsync(id);
            if (actor == null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
