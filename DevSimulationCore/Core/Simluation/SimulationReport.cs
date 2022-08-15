namespace Cbs.DevSimulation.Core.Simulation;

public class SimulationReport : EventArgs
{
    public ProjectSimulation Simulation { get; set; }
    public Team Team { get; set; }
    public DateTime SimulatedCompletion { get; set; }

    public SimulationReport(ProjectSimulation simulation, Team team, DateTime simulatedCompletion)
    {
        Simulation = simulation;
        Team = team;
        SimulatedCompletion = simulatedCompletion;
    }

}