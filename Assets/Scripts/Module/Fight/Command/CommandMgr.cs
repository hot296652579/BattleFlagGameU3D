using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMgr
{
    private Queue<BaseCommand> willDoCommandQueue;//将要执行的命令队列
    private Stack<BaseCommand> unDoStack;//撤销堆栈
    private BaseCommand current;

    public CommandMgr()
    {
        willDoCommandQueue = new Queue<BaseCommand>();
        unDoStack = new Stack<BaseCommand>();
    }

    public bool isRunningCommand
    {
        get
        {
            return current != null;
        }
    }

    public void AddComand(BaseCommand cmd)
    {
        willDoCommandQueue.Enqueue(cmd);
        unDoStack.Push(cmd);
    }  

    public void Update(float dt)
    {
        if(current == null)
        {
            if(willDoCommandQueue.Count > 0)
            {
                current = willDoCommandQueue.Dequeue();
                current.Do();
            }
        }
        else
        {
            if(current.Update(dt) == true)
            {
                current = null;
            }
        }
    }

    //撤销上一个命令
    public void UnDo()
    {
        if(unDoStack.Count > 0)
        {
            unDoStack.Pop().UnDo();
        }
    }

    public void Clear()
    {
        willDoCommandQueue.Clear();
        unDoStack.Clear();
        current = null;
    }
}
