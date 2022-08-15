namespace Cbs.DevSimulation.Core;

public record ProjectSimulation(string Id, Project Project, DateTime Start, Methodology Methodology);

public enum Methodology
{
    Kanban
}