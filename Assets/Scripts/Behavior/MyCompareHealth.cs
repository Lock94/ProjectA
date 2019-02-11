using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BanTiger.Battle;

[TaskDescription("对比血量,血量低返回fail，血量高返回success")]
public class MyCompareHealth : Conditional
{
    public SharedGameObject target;

    public override TaskStatus OnUpdate()
    {
        if (target.Value.GetComponent<NpcController>())
        {
            if (transform.GetComponent<NpcController>().currentHealth< target.Value.GetComponent<NpcController>().currentHealth)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
        if (target.Value.GetComponent<PlayerController>())
        {
            if (transform.GetComponent<NpcController>().currentHealth < target.Value.GetComponent<PlayerController>().currentHealth)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}