using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDesignMode:MonoBehaviour
{
    private void Start()
    {
        Context context = new Context();
        context.SetState(new ConcreteStateA(context));
        context.Handle(5);
        context.Handle(15);
    }
}

public class Context
{
    private IState m_State;

    public void SetState(IState state)
    {
        m_State = state;
    }
    public void Handle(int args)
    {
        m_State.Handle(args);
    }
}
public interface IState
{
    void Handle(int args);      //参数实际需要
}

public class ConcreteStateA : IState
{
    private Context m_Context;
    public ConcreteStateA(Context context)
    {
        m_Context = context;
    }

    public void Handle(int args)
    {
        Debug.Log("ConcreteStateA.Handle"+args);
        if (args>10)
        {
            m_Context.SetState(new ConcreteStateB(m_Context));
        }
    }
}
public class ConcreteStateB : IState
{
    private Context m_Context;
    public ConcreteStateB(Context context)
    {
        m_Context = context;
    }

    public void Handle(int args)
    {
        Debug.Log("ConcreteStateB.Handle" + args);
        if (args <=10)
        {
            m_Context.SetState(new ConcreteStateA(m_Context));
        }
    }
}