using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : BaseController
{
    public FightController() : base()
    {
        GameApp.ViewMgr.Register(ViewType.FightView, new ViewInfo()
        {
            PrefabName = "FightView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf
        }); ;

        GameApp.ViewMgr.Register(ViewType.FightSelectHeroView, new ViewInfo()
        {
            PrefabName = "FightSelectHeroView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf,
            Sorting_Order = 1
        });

        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        base.InitModuleEvent();

        RegisterFunc(Defines.BeginFight, onBeginFigthCallBack);
    }

    private void onBeginFigthCallBack(System.Object[] arg)
    {
        GameApp.ViewMgr.Open(ViewType.FightView);
        GameApp.ViewMgr.Open(ViewType.FightSelectHeroView);
    }
}
