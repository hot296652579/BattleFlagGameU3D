using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessgeInfo
{
    public string MsgTxt;
    public System.Action okCallBack;
    public System.Action noCallBack;
}

public class MessageView:BaseView
{
    MessgeInfo info;

    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("okBtn").onClick.AddListener(onOkBtn);
        Find<Button>("noBtn").onClick.AddListener(onNoBtn);
    }

    public override void Open(params object[] args)
    {
        base.Open(args);
        info = args[0] as MessgeInfo;
        Find<Text>("content/txt").text = info.MsgTxt;
    }

    private void onOkBtn()
    {
        info.okCallBack?.Invoke();
    }

    private void onNoBtn()
    {
        info.noCallBack?.Invoke();
        GameApp.ViewMgr.Close(ViewId);
    }
}
