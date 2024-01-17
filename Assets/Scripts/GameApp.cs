using UnityEngine;
using System.Collections;

public class GameApp : Singleton<GameApp>
{
    public static SoundMgr SoundManager;
    public static ControllerMgr ControllerMgr;
    public static ViewMgr ViewMgr;
    public static ConfigMgr ConfigMgr;
    public static CameraMgr CameraMgr;
    public static MessageCenter MessageCenter;
    public static TimerMgr TimerMgr;
    public static FightWorldMgr FightWorldMgr;
    public static MapMgr MapMgr;

    public override void Init()
    {
        base.Init();

        SoundManager = new SoundMgr();
        ControllerMgr = new ControllerMgr();
        ViewMgr = new ViewMgr();
        ConfigMgr = new ConfigMgr();
        CameraMgr = new CameraMgr();
        MessageCenter = new MessageCenter();
        TimerMgr = new TimerMgr();
        FightWorldMgr = new FightWorldMgr();
        MapMgr = new MapMgr();
    }

    public override void Update(float t)
    {
        base.Update(t);
        TimerMgr.OnUpdate(t);
        FightWorldMgr.Update(t);
    }

    public override void onDestory()
    {
        base.onDestory();
    }
}
