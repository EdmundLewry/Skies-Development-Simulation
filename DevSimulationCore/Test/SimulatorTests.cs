using Cbs.DevSimulation.Core.Simulation;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cbs.DevSimulation.Core.Test;

public class SimulatorTests
{
    [Fact]
    public void WhenGivenASingleFeatureProject_ThenTheProjectEndDateShouldTakeOneVelocityPeriod()
    {
        List<Feature> projectFeatures = new()
        {
            new Feature("TestFeature", new List<DevelopmentTask>()
            {
                new DevelopmentTask("TestTask", new List<Requirement>() , 5)
            })
        };

        Project project = new("TestProject", projectFeatures);
        ProjectSimulation projectSimulation = new("Test", project, DateTime.Parse("2022-01-01T12:00:00"), Methodology.Kanban);

        List<Developer> devs = new()
        {
            new("Test McTest", new Dictionary<Skill, Proficiency>(), new Role("Developer", 5))
        };

        Team team = new(devs, 1);

        Simulator simulator = new();

        Assert.Raises<SimulationReport>(
            handler => simulator.OnSimulationEnd += handler, 
            handler => simulator.OnSimulationEnd -= handler, 
            () => simulator.Simulate(projectSimulation, team)
        );
    }
}