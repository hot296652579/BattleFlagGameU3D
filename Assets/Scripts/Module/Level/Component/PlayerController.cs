using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 2.5f;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        GameApp.CameraMgr.SetPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if(h == 0)
        {
            ani.Play("idle");
        }
        else
        {
            if((h > 0 && transform.lossyScale.x < 0) || (h < 0 && transform.localScale.x >0))
            {
                Flip();
            }

            //移动限制范围
            Vector3 pos = transform.position + Vector3.right * h * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32, 24);
            transform.position = pos;
            GameApp.CameraMgr.SetPos(transform.position);
            //transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
            ani.Play("move");
        }
    }

    //转向
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
