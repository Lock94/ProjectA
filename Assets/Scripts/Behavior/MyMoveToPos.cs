using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("动作：移动到指定点/往前走/往后走")]
public class MyMoveToPos : Action
{
    public enum MoveType { none, strafe, foward, back }
    public SharedVector3 destination;
    public MoveType moveType;
}