namespace Cbs.DevSimulation.Core;

public record DevelopmentTask(string Name, IEnumerable<Requirement> Requirements, int Estimate);
