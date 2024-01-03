using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通用ui控制器
public class GameUIController : BaseController
{

    public GameUIController() : base()
    {
        //开始注册视图

        GameApp.ViewMgr.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf
        });

        GameApp.ViewMgr.Register(ViewType.SetView, new ViewInfo()
        {
            PrefabName = "SetView",
            controller = this,
            Sorting_Order = 1,
            parentTf = GameApp.ViewMgr.canvasTf
        });

        GameApp.ViewMgr.Register(ViewType.MessageView, new ViewInfo()
        {
            PrefabName = "MessageView",
            controller = this,
            Sorting_Order = 999,
            parentTf = GameApp.ViewMgr.canvasTf
        });

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView);//注册打开开始界面
        RegisterFunc(Defines.OpenSetView, openSetView);//注册打开设置界面
        RegisterFunc(Defines.OpenMessageView, openMessageView);//注册通用信息弹窗界面
    }

    public override void InitGlobalEvent()
    {
        
    }

    private void openSetView(object[] args)
    {
        GameApp.ViewMgr.Open(ViewType.SetView, args);
    }

    private void openMessageView(object[] args)
    {
        GameApp.ViewMgr.Open(ViewType.MessageView, args);
    }

    //test
    private void openStartView(System.Object[] args)
    {
        GameApp.ViewMgr.Open(ViewType.StartView,args);
    }
}
