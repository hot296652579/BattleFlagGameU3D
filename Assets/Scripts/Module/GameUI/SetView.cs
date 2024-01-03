using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetView :BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bg/closeBtn").onClick.AddListener(onClickClose);
    }

    private void onClickClose()
    {
        GameApp.ViewMgr.Close(ViewId);
    }
}
