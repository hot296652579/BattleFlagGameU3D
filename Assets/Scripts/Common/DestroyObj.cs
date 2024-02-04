using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float timer;
    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
