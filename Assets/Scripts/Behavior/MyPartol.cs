using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

[TaskDescription("动作：巡逻")]
public class MyPartol : Action
{  
    public SharedGameObjectList partolPoints;
    public SharedFloat arriveDistance;

    private Vector3 m_currentPoint;
    private NavMeshAgent m_agent;
    private Animator m_animator;

    public override void OnStart()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.enabled = false;
        m_animator = GetComponent<Animator>();
        if (partolPoints.Value != null && partolPoints.Value.Count > 0 && !partolPoints.Value.Contains(null))
        {
            m_currentPoint = partolPoints.Value[Random.Range(0, partolPoints.Value.Count)].transform.position;
        }
        else
        {
            Debug.LogWarning("路径点有空值！请检查");
        }
    }
    public override TaskStatus OnUpdate()
    {
        if (m_agent!=null&&!m_agent.hasPath&& m_currentPoint!=null)
        {
            m_agent.enabled = true;
            m_agent.SetDestination(m_currentPoint);
            m_agent.stoppingDistance = arriveDistance.Value;
            m_animator.SetFloat("InputMagnitude", 0.5f);
        }
        if (Vector3.Distance(m_currentPoint,transform.position)> arriveDistance.Value)
        {         
            return TaskStatus.Running;
        }
        //m_animator.SetTrigger("IsIdle");
        m_animator.SetFloat("InputMagnitude", 0);
        return TaskStatus.Success;
    }

}