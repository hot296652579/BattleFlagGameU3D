using UnityEngine;
using System.Collections;

//视图接口
public interface IBaseView
{

    bool IsInit();
    bool IsShow();
    void InitUI();
    void InitData();
    void Open(params object[] args);
    void Close(params object[] args);
    void DestroyView();
    void ApplyFunc(string eventName,params object[] args);//触发本模块事件
    void ApplyControllerFunc(int controllerKey,string eventName,params object[] args);//触发其他控制器模块事件

    void SetVisible(bool value);//设置显示隐藏

    int ViewId { set; get; }

    BaseController Controller { set; get; }
}
