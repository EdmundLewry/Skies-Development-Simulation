namespace Cbs.DevSimulation.Core;

public record Developer(string Name, Dictionary<Skill, Proficiency> Capabilities, Role Role);