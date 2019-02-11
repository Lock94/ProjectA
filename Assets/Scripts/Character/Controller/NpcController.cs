using UnityEngine;

public class NpcController : MonoBehaviour
{
    public CharacterProperty NpcProp;
    public int currentHealth = 80;

    private void Start()
    {
        //NpcProp = new HeroPropertyMelee(1, "队长", CharacterPropertyBase.CharacterType.Enemy, 10000, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
    }

    public void ReciveDamage(int damage,Transform sender)
    {
        Debug.Log(sender + "造成" + damage);
    }
    public void OnDeath()
    {

    }
}