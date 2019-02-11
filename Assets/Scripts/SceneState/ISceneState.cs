using UnityEngine;

public class ISceneState 
{
    private SceneStateController m_controller;
    private string m_SceneName;

    public ISceneState(string sceneName, SceneStateController controller)
    {
        this.m_SceneName = sceneName;
        this.m_controller = controller;
    }

    public virtual void StateStart()
    {

    }

    public virtual void StateEnd()
    {

    }

    public virtual void StateUpdate()
    {

    }
}