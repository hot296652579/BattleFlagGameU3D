using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏主控制器
public class GameController : BaseController
{
    public GameController() : base(){
        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
    }
}
