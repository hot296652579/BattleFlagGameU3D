using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : BaseController
{
    AsyncOperation asyncOp;

    public LoadingController() : base(){
        GameApp.ViewMgr.Register(ViewType.LoadingView, new ViewInfo()
        {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameApp.ViewMgr.canvasTf
        });

        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenLoadingView, loadSceneCallBack);
    }

    //加载场景回掉
    private void loadSceneCallBack(System.Object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;
        SetModel(loadingModel);

        GameApp.ViewMgr.Open(ViewType.LoadingView);

        asyncOp = SceneManager.LoadSceneAsync(loadingModel.SceneName);
        asyncOp.completed += onLoadedEndCallBack;
    }

    //加载后回调
    private void onLoadedEndCallBack(AsyncOperation op)
    {
        asyncOp.completed -= onLoadedEndCallBack;
        GetModel<LoadingModel>().callback?.Invoke();
        GameApp.ViewMgr.Close((int)ViewType.LoadingView);
    }
}
