using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public static class ProjectExtensions
{
    public static Project Clone(this Project project)
    {
        return new Project
        {
            Id = project.Id,
            Name = project.Name
        };
    }

    public static void Update(this Project project, Project clone)
    {
        project.Name = clone.Name;
    }
}