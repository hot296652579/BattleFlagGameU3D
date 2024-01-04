using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        Find<Button>("close").onClick.AddListener(OnClickBtn);
    }

    private void OnClickBtn()
    {
        GameApp.ViewMgr.Close(ViewId);

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = delegate ()
        {
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.OpenLoadingView, loadingModel);
    }

    //显示详情面板
    public void ShowLevelDes()
    {
        Find("level").SetActive(true);
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    }

    //隐藏关卡
    public void HideLevelDes()
    {
        Find("level").SetActive(false);
    }
}
