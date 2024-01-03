using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerMgr 
{
    private Dictionary<int, BaseController> _modules;//控制器字典

    public ControllerMgr()
    {
        _modules = new Dictionary<int, BaseController>();
    }

    public void Register(ControllerType type,BaseController ctl)
    {
        Register((int)type, ctl);
    }

    private void Register(int controllerKey,BaseController ctl)
    {
        if (_modules.ContainsKey(controllerKey) == false)
        {
            _modules.Add(controllerKey,ctl);
        }
    }

    //执行所有控制器init
    public void InitAllModules()
    {
        foreach(var item in _modules)
        {
            item.Value.Init();
        }
    }

    public void UnRegister(int controllerKey)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            _modules.Remove(controllerKey);
        }
    }

    public void Clear()
    {
        _modules.Clear();
    }

    public void ClearAllModule()
    {
        List<int> keys = _modules.Keys.ToList();
        for(int i = 0; i < keys.Count; i++)
        {
            _modules[keys[i]].Destory();
            _modules.Remove(keys[i]);
        }
    }

    //跨控制器触发事件
    public void ApplyFunc(int controllerKey,string eventName,object[] args)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        return null;
    }
}
