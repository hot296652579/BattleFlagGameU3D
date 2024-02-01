using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data)
    {
        this.op_data = data;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameApp.MessageCenter.PostEvent(op_data.EventName);
            GameApp.ViewMgr.Close((int)ViewType.SelectOptionView);
        });

        transform.Find("txt").GetComponent<Text>().text = op_data.Name;
    }
}
