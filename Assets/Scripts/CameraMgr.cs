using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr 
{
    private Transform camTf;
    private Vector3 perPos;//之前位置

    public CameraMgr()
    {
        camTf = Camera.main.transform;
        perPos = camTf.transform.position;
    }

    public void SetPos(Vector3 pos)
    {
        pos.z = camTf.position.z;
        camTf.transform.position = pos;
    }

    public void ResetPos()
    {
        camTf.transform.position = perPos;
    }
}
