using UnityEngine;
using System.Collections.Generic;

public class TeamController : MonoBehaviour
{
    public NpcController m_Leader;

    private List<NpcController> m_Soldiers;

    public List<NpcController> Soliders
    {
        get
        {
            if (m_Soldiers == null)
            {
                m_Soldiers = new List<NpcController>();
            }
            return m_Soldiers;
        }
        set { m_Soldiers = value; }
    }
    /// <summary>
    /// 建立小队
    /// </summary>
    /// <param name="npcPrefab"></param>
    /// <param name="npcProp"></param>
    public void BuildSoliders(GameObject npcPrefab, CharacterProperty npcProp)
    {
        if (Soliders.Count >=4&&m_Leader!=null)
        {
            Debug.LogWarning("这个小队满员了");
            return;
        }
        GameObject npc = Instantiate(npcPrefab);
        npc.transform.SetParent(transform);
        if (m_Leader ==null&& Soliders.Count==0 )
        {
            npc.transform.localPosition = Vector3.zero;
        }
        if (m_Leader!=null)
        {
            switch (Soliders.Count)
            {
                case 0:
                    npc.transform.localPosition = Vector3.right;
                    break;
                case 1:
                    npc.transform.localPosition = Vector3.left;
                    break;
                case 2:
                    npc.transform.localPosition = Vector3.forward;
                    break;
                case 3:
                    npc.transform.localPosition = Vector3.back;
                    break;
            } 
        }
        npc.GetComponent<NpcController>().NpcProp = npcProp;
        //if (npcProp.)
        //{
        //    m_Leader = npc.GetComponent<NpcController>();
        //}
        //else
        //{
        //    Soliders.Add(npc.GetComponent<NpcController>());
        //}
    }
}