  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : BaseController
{
    public LevelController() : base(){

        SetModel(new LevelModel());

        GameApp.ViewMgr.Register(ViewType.SelectLevelView, new ViewInfo()
        {
            PrefabName = "SelectLevelView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf
        });

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        base.Init();
        model.Init();
    }

    public override void InitModuleEvent()
    {
        base.InitModuleEvent();
        RegisterFunc(Defines.OpenSelectLevelView, onOpenSelectLevelView);
    }

    public override void InitGlobalEvent()
    {
        GameApp.MessageCenter.AddEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameApp.MessageCenter.AddEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);
    }

    public override void RemoveGlobalEvent()
    {
        base.RemoveGlobalEvent();
        GameApp.MessageCenter.RemoveEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);
    }

    private void onShowLevelDesCallBack(System.Object args)
    {
        Debug.Log("levelId:" + args.ToString());

        LevelModel levelModel = GetModel<LevelModel>();
        levelModel.current = levelModel.GetLevel(int.Parse(args.ToString()));

        GameApp.ViewMgr.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDes();
    }

    private void onHideLevelDesCallBack(System.Object args)
    {
        GameApp.ViewMgr.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDes();
    }

    public void onOpenSelectLevelView(System.Object[] args)
    {
        GameApp.ViewMgr.Open(ViewType.SelectLevelView, args);
    }
}
 