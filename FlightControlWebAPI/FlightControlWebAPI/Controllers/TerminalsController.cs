using FlightControlWebAPI.DAL;
using FlightControlWebAPI.Models;
using FlightControlWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalsController : ControllerBase
    {
        private readonly ITerminalService _terminalService;
        public TerminalsController(ITerminalService data) => this._terminalService = data;

        [HttpGet]
        public async Task<IEnumerable<Terminal>> GetTerminals() => await _terminalService.GetTerminals();
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTerminalById(int id)
        {
            var terminal = await _terminalService.GetTerminalById(id);
            return terminal == null ? NotFound() : Ok(terminal);
        }
    }
}
