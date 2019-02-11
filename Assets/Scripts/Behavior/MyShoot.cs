using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;

[TaskDescription("动作：射击")]
public class MyShoot : Action
{
    public SharedGameObject target;
    public SharedInt leftAmmos;
    public SharedFloat shootInterval;
    public SharedFloat ReloadTime;

    private int shootTimes;
    private bool isShooting;

    public override void OnStart()
    {
        shootTimes = Random.Range(4, 7);
        Debug.Log(shootTimes);
    }

    public override TaskStatus OnUpdate()
    {
        //TODO 检查是否有子弹,包含换弹

        if (shootTimes>0&& isShooting)
        {
            StartCoroutine(ShootAction(1));
        }
        return TaskStatus.Success;
    }
    IEnumerator ShootAction(float interval)
    {
        //TODO shoot
        isShooting = true;
        yield return new WaitForSeconds(interval);
        shootTimes--;
        leftAmmos.Value--;
        isShooting = false;
    }

}