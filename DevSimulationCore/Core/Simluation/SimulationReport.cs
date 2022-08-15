namespace Cbs.DevSimulation.Core.Simulation;

public class SimulationReport : EventArgs
{
    public ProjectSimulation? Simulation { get; set; } = null;
    public Team? Team { get; set; } = null;
    public DateTime SimulatedCompletion { get; set; } = DateTime.MinValue;

}