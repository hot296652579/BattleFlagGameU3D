using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionData
{
    public int Id;
    public string EventName;
    public string Name;
}

//战斗用的数据
public class FightModel : BaseModel
{
    public List<OptionData> options;
    public ConfigData optionConfig;

    public FightModel(BaseController ctl) : base(ctl)
    {
        options = new List<OptionData>();
    }

    public override void Init()
    {
        base.Init();
        optionConfig = GameApp.ConfigMgr.GetConfigDdata("option");
        foreach(var item in optionConfig.GetLines())
        {
            OptionData opData = new OptionData();
            opData.Id = int.Parse(item.Value["Id"]);
            opData.Name = item.Value["Name"];
            opData.EventName = item.Value["EventName"];
            options.Add(opData);
        }
    }
}
