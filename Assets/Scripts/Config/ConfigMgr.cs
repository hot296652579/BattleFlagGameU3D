using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

//管理游戏配置表
public class ConfigMgr
{
    private Dictionary<string, ConfigData> loadList;//需要读取的配置表
    private Dictionary<string, ConfigData> configs;//已经加载完的配置表

    public ConfigMgr()
    {
        loadList = new Dictionary<string, ConfigData>();
        configs = new Dictionary<string, ConfigData>();
    }

    public void Register(string file,ConfigData config)
    {
        loadList[file] = config;
    }

    public void LoadAllConfigs()
    {
        foreach(var item in loadList)
        {
            TextAsset textAsset = item.Value.LoadFile();
            item.Value.Load(textAsset.text);
            configs.Add(item.Value.fileName, item.Value);
        }

        loadList.Clear();
    }

    public ConfigData GetConfigDdata(string file)
    {
        if (configs.ContainsKey(file))
        {
            return configs[file];
        }
        else
        {
            return null;
        }
    }
}
