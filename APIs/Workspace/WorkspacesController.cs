using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyService.APIs.Workspace.Dtos;
using MyService.Infrastructure;

namespace MyService.APIs.Workspace
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        private readonly MyServiceContext _context;

        public WorkspacesController(MyServiceContext context)
        {
            _context = context;
        }

        // GET: api/Workspaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkspaceDto>>> GetWorkspaces()
        {
            var workspaces = await _context.Workspaces.ToListAsync();

            return workspaces.ConvertAll(w => new WorkspaceDto
            {
                Id = w.Id,
                Name = w.Name
            });
        }

        // GET: api/Workspaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkspaceDto>> GetWorkspace(long id)
        {
            var workspace = await _context.Workspaces.FindAsync(id);

            if (workspace == null)
            {
                return NotFound();
            }

            return new WorkspaceDto
            {
                Id = workspace.Id,
                Name = workspace.Name
            };
        }

        // PUT: api/Workspaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkspace(long id, WorkspaceDto workspaceDto)
        {
            if (id != workspaceDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(workspaceDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkspaceExists(id))
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

        // POST: api/Workspaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkspaceDto>> PostWorkspace(WorkspaceDto workspaceDto)
        {
            var workspace = new Infrastructure.Models.Workspace()
            {
                Name = workspaceDto.Name,
                Id = workspaceDto.Id
            };
            _context.Workspaces.Add(workspace);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkspace), new { id = workspaceDto.Id }, workspaceDto);
        }

        // DELETE: api/Workspaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace(long id)
        {
            var workspaceDto = await _context.Workspaces.FindAsync(id);
            if (workspaceDto == null)
            {
                return NotFound();
            }

            _context.Workspaces.Remove(workspaceDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkspaceExists(long id)
        {
            return _context.Workspaces.Any(e => e.Id == id);
        }
    }
}
