using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : BaseController
{
    public FightController() : base()
    {
        SetModel(new FightModel(this));

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

        GameApp.ViewMgr.Register(ViewType.DragHeroView, new ViewInfo()
        {
            PrefabName = "DragHeroView",
            controller = this,
            parentTf = GameApp.ViewMgr.worldCanvasTf,//设置到世界画布
            Sorting_Order = 2
        });

        GameApp.ViewMgr.Register(ViewType.TipView, new ViewInfo()
        {
            PrefabName = "TipView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf,
            Sorting_Order = 2
        });

        GameApp.ViewMgr.Register(ViewType.HeroDesView, new ViewInfo()
        {
            PrefabName = "HeroDesView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf,
            Sorting_Order = 2
        });

        GameApp.ViewMgr.Register(ViewType.EnemyDesView, new ViewInfo()
        {
            PrefabName = "EnemyDesView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf,
            Sorting_Order = 2
        });

        GameApp.ViewMgr.Register(ViewType.SelectOptionView, new ViewInfo()
        {
            PrefabName = "SelectOptionView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf
        });

        GameApp.ViewMgr.Register(ViewType.FightOptionDesView, new ViewInfo()
        {
            PrefabName = "FightOptionDesView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf,
            Sorting_Order = 3
        });

        InitModuleEvent();
    }

    public override void Init()
    {
        model.Init();
    }

    public override void InitModuleEvent()
    {
        base.InitModuleEvent();

        RegisterFunc(Defines.BeginFight, onBeginFigthCallBack);
    }

    private void onBeginFigthCallBack(System.Object[] arg)
    {
        //进入战斗
        GameApp.FightWorldMgr.ChangeState(GameState.Enter);

        GameApp.ViewMgr.Open(ViewType.FightView);
        GameApp.ViewMgr.Open(ViewType.FightSelectHeroView);
    }
}
