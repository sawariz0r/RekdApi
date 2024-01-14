using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RekdApi.Models;

namespace RekdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionsController : ControllerBase
    {
        private readonly GameDbContext _context;

        public GameSessionsController(GameDbContext context)
        {
            _context = context;
        }

        // GET: api/GameSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSession>>> GetGameSessions()
        {
            return await _context.GameSessions.ToListAsync();
        }

        // GET: api/GameSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSession>> GetGameSession(long id)
        {
            var gameSession = await _context.GameSessions.FindAsync(id);

            if (gameSession == null)
            {
                return NotFound();
            }

            return gameSession;
        }

        // PUT: api/GameSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameSession(long id, GameSession gameSession)
        {
            if (id != gameSession.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameSession>> PostGameSession(GameSession gameSession)
        {
            _context.GameSessions.Add(gameSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameSession", new { id = gameSession.Id }, gameSession);
        }



        // POST: api/GameSessions/5/Move
        [HttpPost("{id}/Move")]
        public async Task<IActionResult> PostMove(long id)
        {

            // Mock long 
            long mockId = 1;
            var gameSession = await _context.GameSessions.FindAsync(mockId);
            if (gameSession == null)
            {
                return NotFound();
            }

            // TODO: Handle the move

            // After handling the move, save changes to db
            await _context.SaveChangesAsync();

            // TODO: Send a notification to the other player. 
            // We dont need to wait for this to finish, so we can just fire and forget
            var sendExpoNotification = new SendExpoNotification();
            _ = sendExpoNotification.SendNotification("ExponentPushToken[xxxxxxxxxxxxxxxxxxxxxx]");


            return NoContent();
        }

        // DELETE: api/GameSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSession(long id)
        {
            var gameSession = await _context.GameSessions.FindAsync(id);
            if (gameSession == null)
            {
                return NotFound();
            }

            _context.GameSessions.Remove(gameSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameSessionExists(long id)
        {
            return _context.GameSessions.Any(e => e.Id == id);
        }
    }
}
