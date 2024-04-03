using Microsoft.EntityFrameworkCore;
using MyService.APIs.Errors;
using MyService.APIs.Workspace.Dtos;
using MyService.Infrastructure;

public class WorkspacesService : IWorkspacesService
{
    private readonly MyServiceContext _context;

    public WorkspacesService(MyServiceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkspaceDto>> Workspaces()
    {
        var workspaces = await _context.Workspaces.ToListAsync();

        return workspaces.ConvertAll(w => new WorkspaceDto
        {
            Id = w.Id,
            Name = w.Name
        });
    }

    public async Task<WorkspaceDto> Workspace(long id)
    {
        var workspace = await _context.Workspaces.FindAsync(id);

        if (workspace == null)
        {
            throw new NotFoundException();
        }

        return new WorkspaceDto
        {
            Id = workspace.Id,
            Name = workspace.Name
        };
    }

    public async Task UpdateWorkspace(long id, WorkspaceDto workspaceDto)
    {
        _context.Entry(workspaceDto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkspaceExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<WorkspaceDto> CreateWorkspace(WorkspaceDto workspaceDto)
    {
        var workspace = new MyService.Infrastructure.Models.Workspace()
        {
            Name = workspaceDto.Name,
            Id = workspaceDto.Id
        };
        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync();

        return workspaceDto;
    }

    public async Task DeleteWorkspace(long id)
    {
        var workspaceDto = await _context.Workspaces.FindAsync(id);
        if (workspaceDto == null)
        {
            throw new NotFoundException();
        }

        _context.Workspaces.Remove(workspaceDto);
        await _context.SaveChangesAsync();
    }

    private bool WorkspaceExists(long id)
    {
        return _context.Workspaces.Any(e => e.Id == id);
    }
}