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

    public override void Init()
    {
        base.Init();

        SoundManager = new SoundMgr();
        ControllerMgr = new ControllerMgr();
        ViewMgr = new ViewMgr();
        ConfigMgr = new ConfigMgr();
        CameraMgr = new CameraMgr();
        MessageCenter = new MessageCenter();
    }

    public override void Update(float t)
    {
        base.Update(t);
    }

    public override void onDestory()
    {
        base.onDestory();
    }
}
