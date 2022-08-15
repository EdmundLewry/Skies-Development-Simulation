using Cbs.DevSimulation.Core.Simulation;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cbs.DevSimulation.Core.Test;

public class SimulatorTests
{
    private static ProjectSimulation CreateTestProjectSimulation(List<Feature>? features = null)
    {
        List<Feature> projectFeatures = features ?? new()
        {
            new Feature("TestFeature", new List<DevelopmentTask>()
            {
                new DevelopmentTask("TestTask", new List<Requirement>() , 5)
            })
        };

        Project project = new("TestProject", projectFeatures);
        ProjectSimulation projectSimulation = new("Test", project, DateTime.Parse("2022-01-03T12:00:00"), Methodology.Kanban);

        return projectSimulation;
    }

    private static Team CreateTestTeam()
    {
        List<Developer> devs = new()
        {
            new("Test McTest", new Dictionary<Skill, Proficiency>(), new Role("Developer", 5))
        };

        Team team = new(devs, 1);

        return team;
    }

    [Fact]
    public void WhenSimulationIsRunAndEnds_ThenItRaisesASimulationReport()
    {
        Simulator simulator = new();

        Assert.Raises<SimulationReport>(
            handler => simulator.OnSimulationEnd += handler, 
            handler => simulator.OnSimulationEnd -= handler, 
            () => simulator.Simulate(CreateTestProjectSimulation(), CreateTestTeam())
        );
    }

    [Fact]
    public void WhenSingleTaskProjectIsSimulated_ThenExpectedEndDateIsCalculated()
    {
        Simulator simulator = new();

        DateTime expectedCompletion = DateTime.Parse("2022-01-14T12:00:00");
        DateTime completion = DateTime.MinValue;

        simulator.OnSimulationEnd += (sender, report) =>
        {
            completion = report.SimulatedCompletion;
        };

        simulator.Simulate(CreateTestProjectSimulation(), CreateTestTeam());

        Assert.Equal(expectedCompletion, completion);
    }
    
    [Fact]
    public void WhenTheProjectContainsMultipleTasksAndOneDeveloper_ThenExpectedEndDateIsCalculated()
    {
        Simulator simulator = new();

        DateTime expectedCompletion = DateTime.Parse("2022-01-20T12:00:00");
        DateTime completion = DateTime.MinValue;

        simulator.OnSimulationEnd += (sender, report) =>
        {
            completion = report.SimulatedCompletion;
        };

        List<Feature> projectFeatures = new()
        {
            new Feature("TestFeature", new List<DevelopmentTask>()
            {
                new DevelopmentTask("TestTask", new List<Requirement>() , 5),
                new DevelopmentTask("DependentTask", new List<Requirement>() , 2)
            })
        };
        ProjectSimulation simulation = CreateTestProjectSimulation(projectFeatures);
        

        simulator.Simulate(simulation, CreateTestTeam());

        Assert.Equal(expectedCompletion, completion);
    }
}