using System.Collections.Generic;

public class MissionModel
{
    public string MissionTitle { get; set; }
    public List<ObjectiveModel> Objectives { get; set; } = new();
}

public class ObjectiveModel
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
