using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("能否找到掩体")]
public class MyCanFindHide : Conditional
{
    public SharedVector3 returnVector3;
}