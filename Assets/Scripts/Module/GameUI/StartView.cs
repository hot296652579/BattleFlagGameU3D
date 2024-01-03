using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartView : BaseView 
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("startBtn").onClick.AddListener(onClickStartBtn);
        Find<Button>("setBtn").onClick.AddListener(onClickSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onClickQuitBtn);
    }

    private void onClickStartBtn()
    {
        GameApp.ViewMgr.Close(ViewId);

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "map";
        loadingModel.callback = delegate ()
        {
            Controller.ApplyControllerFunc(ControllerType.Level, Defines.OpenSelectLevelView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.OpenLoadingView, loadingModel);
    }

    private void onClickSetBtn()
    {
        ApplyFunc(Defines.OpenSetView);
    }

    private void onClickQuitBtn()
    {
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessgeInfo()
        {
            okCallBack = delegate
            {
                Application.Quit();
            },
            MsgTxt = "确定退出?"
        }) ;
    }
}
