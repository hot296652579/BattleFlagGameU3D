using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    float dt;
    private static bool isLoaded = false;
    private void Awake()
    {
        if (isLoaded == true)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameApp.SoundManager.PlayBgm("login");

        ResiterConfigs();//注册配置表
        GameApp.ConfigMgr.LoadAllConfigs();//加载配置表

        //测试配置表
        ConfigData tempData = GameApp.ConfigMgr.GetConfigDdata("enemy");
        string name = tempData.GetDataById(10001)["Name"];
        Debug.Log(name);

        RegisterModule();//注册游戏中的控制器
        InitModule();
    }

    void RegisterModule()
    {
        GameApp.ControllerMgr.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerMgr.Register(ControllerType.Game, new GameController());
        GameApp.ControllerMgr.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerMgr.Register(ControllerType.Level, new LevelController());
        GameApp.ControllerMgr.Register(ControllerType.Fight, new FightController());
    }

    //执行所有控制器初始化
    void InitModule()
    {
        GameApp.ControllerMgr.InitAllModules();
    }


    //注册配置表
    void ResiterConfigs()
    {
        GameApp.ConfigMgr.Register("enemy", new ConfigData("enemy"));
        GameApp.ConfigMgr.Register("level", new ConfigData("level"));
        GameApp.ConfigMgr.Register("option", new ConfigData("option"));
        GameApp.ConfigMgr.Register("player", new ConfigData("player"));
        GameApp.ConfigMgr.Register("role", new ConfigData("role"));
        GameApp.ConfigMgr.Register("skill", new ConfigData("skill"));
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}