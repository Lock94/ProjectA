using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("对比距离")]
public class MyCompareDistance : Conditional
{
    public SharedGameObject target;
    public float minDistance;
    public float maxDistance;


    public override TaskStatus OnUpdate()
    {
        float targetTo = Vector3.Distance(target.Value.transform.position, transform.position);
        if (targetTo > minDistance&&targetTo <=maxDistance)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}