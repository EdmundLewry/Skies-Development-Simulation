namespace Cbs.DevSimulation.Core.Simulation;

public class Simulator
{
    private const int VelocityPeriodInWorkingDays = 10;

    public event EventHandler<SimulationReport>? OnSimulationEnd;

    public void Simulate(ProjectSimulation simulation, Team team)
    {
        Project project = simulation.Project;
        DateTime completionDate = simulation.Start.AddDays(-1); //Subtract one day from the completion because we treat the first day of the project as work day
        
        foreach (var feature in project.Features)
        {
            foreach (var task in feature.Tasks)
            {
                double completionQuotient = (double) task.Estimate / team.Members.Select(member => member.Role.BaseVelocity).Sum();
                int daysToCompletion = (int) Math.Ceiling(completionQuotient * VelocityPeriodInWorkingDays);
                int weekendDaysInPeriod = CountWeekendDays(completionDate, daysToCompletion);

                completionDate = completionDate.AddDays(daysToCompletion + weekendDaysInPeriod);
            }
        }

        OnSimulationEnd?.Invoke(this, new SimulationReport(simulation, team, completionDate));
    }

    private static int CountWeekendDays(DateTime completionDate, int daysToCompletion)
    {
        int count = 0;
        for(int i = 0; i < daysToCompletion; ++i)
        {
            completionDate = completionDate.AddDays(1);
            if(completionDate.DayOfWeek == DayOfWeek.Saturday || 
               completionDate.DayOfWeek == DayOfWeek.Sunday)
            {
                count++;
            }
        }

        return count;
    }
}
