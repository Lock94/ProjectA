using UnityEngine;
using System.Collections.Generic;

public class TeamBigPanel : BigPanel
{
    #region 单例模式
    private static TeamBigPanel _instance;
    public static TeamBigPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("TeamBigPanel").GetComponent<TeamBigPanel>();
            }
            return _instance;
        }
    }
    #endregion

    //private PlayerInventory player;

    private TeamPanel teamPanel;
    private CharacterPanel charPanel;

    private List<Item> tempList;

    public override void Start()
    {
        base.Start();
       // player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        teamPanel = transform.Find("TeamPanel").GetComponent<TeamPanel>();
        charPanel = transform.Find("TeamCharPanel").GetComponent<CharacterPanel>();
    }


    public override void Show()
    {
        base.Show();
        //------------背包----------------
        tempList = BackPack.Instance.AllItems();
        BackPack.Instance.ClearItems();
        BackPack.BackPackID = 3;
        BackPack.Instance.TransferItems(tempList);
        //------------背包----------------

        //------------角色----------------
        CharacterProperty player = PropertyManager.Instance.AllCharacters[0];

        CharacterPanel.Instance.ClearEquipments();
        CharacterPanel.charPanelID = 2;
        Debug.Log(player.WeaponWith.Name);
        CharacterPanel.Instance.SetEquipments(player.EquipmentProp.HeadID, player.EquipmentProp.ClothID, player.EquipmentProp.PantsID, player.EquipmentProp.BeltID, player.WeaponWith);
        PropertyManager.Instance.SetTextShow();

        teamPanel.ClearAllTeam();
        for (int i = 0; i < PropertyManager.Instance.CompanionCharBase.Count; i++)
        {
            teamPanel.SetNewTeam(PropertyManager.Instance.CompanionCharBase[i]);
        }
        CharacterPanel.Instance.CheckAllSlot();
        //------------角色----------------
    }
    public override void Hide()
    {
        base.Hide();
        //------------背包----------------
        tempList = BackPack.Instance.AllItems();
        BackPack.Instance.ClearItems();
        BackPack.BackPackID = 1;
        BackPack.Instance.TransferItems(tempList);
        //------------背包----------------

        //------------角色----------------
        CharacterProperty chosenChar = PropertyManager.Instance.AllCharacters[0];
        //Debug.Log("HIDE" + chosenChar.Name);
        PropertyManager.Instance.ChangeCharProp(chosenChar);
        CharacterPanel.Instance.ClearEquipments();
        CharacterPanel.charPanelID = 1;
        CharacterPanel.Instance.SetEquipments(chosenChar.EquipmentProp.HeadID, chosenChar.EquipmentProp.ClothID, chosenChar.EquipmentProp.PantsID, chosenChar.EquipmentProp.BeltID, chosenChar.WeaponWith);
        PropertyManager.Instance.SetTextShow();
        CharacterPanel.Instance.CheckAllSlot();
        //------------角色----------------
    }

    public void ChangeMessage(CharacterProperty charProp)
    {
        CharacterProperty charChoose = PropertyManager.Instance.GetCharByID(charProp.ID);
        CharacterPanel.Instance.ClearEquipments();
        Debug.Log(charChoose.WeaponWith.Name);
        PropertyManager.Instance.ChangeCharProp(charChoose);
        CharacterPanel.Instance.SetEquipments(charChoose.EquipmentProp.HeadID, charChoose.EquipmentProp.ClothID, charChoose.EquipmentProp.PantsID, charChoose.EquipmentProp.BeltID, charChoose.WeaponWith);
        CharacterPanel.Instance.CheckAllSlot();
    }
}