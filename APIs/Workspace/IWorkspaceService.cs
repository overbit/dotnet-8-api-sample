using MyService.APIs.Workspace.Dtos;

public interface IWorkspacesService
{
    public Task<IEnumerable<WorkspaceDto>> GetWorkspaces();

    public Task<WorkspaceDto> GetWorkspace(long id);

    public Task PutWorkspace(long id, WorkspaceDto workspaceDto);

    public Task<WorkspaceDto> PostWorkspace(WorkspaceDto workspaceDto);
    public Task DeleteWorkspace(long id);

}