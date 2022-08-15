namespace Cbs.DevSimulation.Core.Simulation;

public class Simulator
{
    private const int VelocityPeriodInWorkingDays = 10;

    public event EventHandler<SimulationReport>? OnSimulationEnd;

    public void Simulate(ProjectSimulation simulation, Team team)
    {
        Project project = simulation.Project;
        DateTime completionDate = simulation.Start;

        foreach (var feature in project.Features)
        {
            foreach (var task in feature.Tasks)
            {
                
            }
        }
    }
}
