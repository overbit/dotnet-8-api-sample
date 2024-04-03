using MyService.APIs.Workspace.Dtos;

public interface IWorkspacesService
{
    public Task<IEnumerable<WorkspaceDto>> Workspaces();

    public Task<WorkspaceDto> Workspace(long id);

    public Task UpdateWorkspace(long id, WorkspaceDto workspaceDto);

    public Task<WorkspaceDto> CreateWorkspace(WorkspaceDto workspaceDto);
    public Task DeleteWorkspace(long id);

}