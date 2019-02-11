using UnityEngine;
using System.Collections.Generic;

public class TeamPanel : MonoBehaviour
{
    public GameObject TeamSlotPFB;
    private List<TeamSlot> teamList;
    private Transform content;

    void Start()
    {
        teamList = new List<TeamSlot>();
        content = transform.GetChild(0);
        foreach (Transform child in content)
        {
            teamList.Add(child.GetComponent<TeamSlot>());
        }
        //测试
        //CharacterProperty[] charProps = new CharacterProperty[5];
        //SetNewTeam(charProps);
    }

    public void ClearAllFrame()
    {
        foreach (TeamSlot team in teamList)
        {
            team.CloseAllFrame();
        }
    }
    public void SetNewTeam(CharacterProperty[] charProps)
    {
        GameObject teamGo = Instantiate(TeamSlotPFB);
        teamGo.transform.SetParent(content);
        teamGo.transform.localScale = new Vector3(1, 1, 1);
        TeamSlot teamslot = teamGo.GetComponent<TeamSlot>();
        teamslot.SetPropertys(charProps);
        teamList.Add(teamslot);
    }
    public void ClearAllTeam()
    {
        for (int i = 0; i < teamList.Count; i++)
        {
            TeamSlot tempSlot = teamList[i];
            DestroyImmediate(tempSlot.gameObject);
        }
        teamList = new List<TeamSlot>();
       // teamList.Add(content.GetChild(0).GetComponent<TeamSlot>());
    }
    public void ChangeTeam(CharacterProperty[] charProps,int id) //0是玩家
    {
        teamList[id].SetPropertys(charProps);
    }
    public int GetTeamCount()
    {
        return teamList.Count;
    }
}