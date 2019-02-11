using UnityEngine;
using UnityEngine.UI;

public class TeamSlot : MonoBehaviour 
{
    private CharacterSlot[] members;
    private CharacterSlot[] Members
    {
        get
        {
            if (members ==null)
            {
                members = transform.GetComponentsInChildren<CharacterSlot>();
            }
            return members;
        }
    }

    public void CloseAllFrame()
    {
        foreach (CharacterSlot  slot in Members)
        {
            slot.CloseFrame();
        }
    }
    public void CheckFrame()
    {
        transform.parent.parent.SendMessage("ClearAllFrame");
    }

    public void SetPropertys(CharacterProperty[] charProps)
    {
        if (charProps.Length != 5)
        {
            Debug.LogError("小队的数目出现差错!");
            return;
        }
        for (int i = 0; i < charProps.Length; i++)
        {
            Members[i].SetCharProp(charProps[i]);
        }
    }
}