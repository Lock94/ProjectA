using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("能否近战：近战武器/枪没子弹/距离过近")]
public class MyCanMelee : Conditional
{
    public SharedGameObject target;
    public float meleeRadius;
    public SharedInt leftAmoos;

    public override TaskStatus OnUpdate()
    {
        if (target.Value =null)
        {
            return TaskStatus.Failure;
        }
        if (Vector3.Distance(target.Value.transform.position, transform.position) <= meleeRadius)
        {
            return TaskStatus.Success;
        }
        //if (GetComponent<NpcController>().NpcProp is HeroPropertyMelee || GetComponent<NpcController>().NpcProp is CharacterPropertyMelee)
        //{
        //    return TaskStatus.Success;
        //}
        if (leftAmoos.Value <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}