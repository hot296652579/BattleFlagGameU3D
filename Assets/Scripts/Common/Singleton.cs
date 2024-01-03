using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    //初始化
    public virtual void Init()
    {

    }

    public virtual void Update(float t)
    {

    }

    public virtual void onDestory()
    {

    }
}
