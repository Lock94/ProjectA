using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskDescription("能否看见敌人")]
public class MyCanSee : Conditional
{
    public SharedGameObjectList targets;

    public SharedGameObject returnTarget;

    public SharedFloat viewDistance;

    public SharedFloat viewAngle;

    public override TaskStatus OnUpdate()
    {
        if (targets.Value.Count > 0)
        {
            foreach (GameObject target in targets.Value)
            {
                if (target ==null)
                {
                    return TaskStatus.Failure;
                }
                if (IsInView(target))
                {
                    Ray ray = new Ray(transform.position + Vector3.up, target.transform.position - transform.position);
                    Debug.DrawRay(transform.position + Vector3.up, target.transform.position - transform.position, Color.blue);
                    var hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.collider.name);
                        //如果碰撞点属于玩家对象，则表示看到，反之没有看到
                        if (hit.collider && hit.collider.gameObject == target)
                        {
                            Debug.Log("I SEE YOU BABY");
                            returnTarget.Value = target;
                            return TaskStatus.Success;
                        }
                    }
                }
            }
        }
       // Debug.Log("I SEE NOTHING");
        return TaskStatus.Failure;
    }
    //判断目标是否在视线范围内
    bool IsInView(GameObject target)
    {
        float disnatce = Vector3.Distance(transform.position, target.transform.position);
        float angle = Vector3.Angle(transform.forward, (target.transform.position - transform.position));

        //Debug.Log(angle);
        if (angle <=viewAngle.Value&&disnatce<=viewDistance.Value)
        {
            return true;
        }
        return false;
    }
}