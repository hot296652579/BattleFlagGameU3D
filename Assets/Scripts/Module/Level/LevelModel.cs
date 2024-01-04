using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData
{
    public int Id;
    public string Name;
    public string SceneName;
    public string Des;
    public bool IsFinish;

    public LevelData(Dictionary<string ,string> data)
    {
        Id = int.Parse(data["Id"]);
        Name = data["Name"];
        SceneName = data["SceneName"];
        Des = data["Des"];
        IsFinish = false;
    }
}

//关卡数据 
public class LevelModel : BaseModel
{
    private ConfigData levelConfig;
    Dictionary<int, LevelData> levels;
    public LevelData current;

    public LevelModel()
    {
        levels = new Dictionary<int, LevelData>();
    }

    public override void Init()
    {
        //base.Init();
        levelConfig = GameApp.ConfigMgr.GetConfigDdata("level");
        foreach(var item in levelConfig.GetLines())
        {
            LevelData l_data = new LevelData(item.Value);
            levels.Add(l_data.Id, l_data);
        }
    }

    public LevelData GetLevel(int id)
    {
        return levels[id];
    }
}
