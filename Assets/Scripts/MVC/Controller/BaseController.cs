using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**控制器基类 */
public class BaseController
{

    private Dictionary<string, System.Action<object[]>> message;//事件字典

    protected BaseModel model;

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }

    //所有控制器注册后 初始化
    public virtual void Init()
    {

    }

    public virtual void OnLoadView(IBaseView view)
    {

    }

    public virtual void OpenView(IBaseView view)
    {

    }

    public virtual void CloseView(IBaseView view)
    {

    }

    public void RegisterFunc(string eventName,System.Action<object[]> callback)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName] += callback;
        }
        else
        {
            message.Add(eventName, callback);
        }
    }

    public void UnRegisterFunc(string eventName)
    {
        if (message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }

    //触发本模块事件
    public void ApplyFunc(string eventName,params object[] args)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName].Invoke(args);
        }
        else
        {
            Debug.Log("触发本模块事件 Error:" + eventName);
        }
    }

    //触发其他模块事件
    public void ApplyControllerFunc(int controllerKey, string eventName,params object[] args)
    {
        GameApp.ControllerMgr.ApplyFunc(controllerKey, eventName, args);
    }

    /// </summary>
    /// <param name="type">跨模块触发事件</param>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public void ApplyControllerFunc(ControllerType type,string eventName,params object[] args)
    {
        ApplyControllerFunc((int)type, eventName, args);
    }

    public void SetModel(BaseModel model)
    {
        this.model = model;
        this.model.controller = this;
    }

    public BaseModel GetModel()
    {
        return this.model;
    }

    public T GetModel<T>() where T : BaseModel
    {
        return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
        return GameApp.ControllerMgr.GetControllerModel(controllerKey);
    }

    public virtual void Destory()
    {
        RemoveGlobalEvent();
        RemoveModuleEvent();
    }

    //初始化模块事件
    public virtual void InitModuleEvent()
    {

    }

    public virtual void RemoveModuleEvent()
    {

    }

    //初始化全局事件
    public virtual void InitGlobalEvent()
    {

    }

    public virtual void RemoveGlobalEvent()
    {

    }
}
