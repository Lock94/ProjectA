using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成配方
/// </summary>
public class CraftRecipe
{
    public int ID { get; set; }
    public string Name { get; set; }
    public bool IsNeedLearn { get; set; }
    public CraftType Type { get; set; }
    public int[] NeedItemIDs { get; set; }
    public int[] NeedItemCount { get; set; }
    public int[] GetItemIDs { get; set; }
    public int[] GetItemCount { get; set; }

    public CraftRecipe(int id,string name, bool isNeedLearn, CraftType type ,int[] needItemIDs,int[] needItemCount,int[]getItemIDs,int[] getItemCount)
    {
        this.ID = id;
        this.Name = name;
        this.IsNeedLearn = isNeedLearn;
        this.Type = type;
        this.NeedItemIDs = needItemIDs;
        this.NeedItemCount = needItemCount;
        this.GetItemIDs = getItemIDs;
        this.GetItemCount = getItemCount;
    }
    public enum CraftType
    {
        生存,药物,武器,维修,拆解
    }
    public override string ToString()
    {
        string text = string.Format("ID:{0}\nName:{1}\nIsNeedLearn:{2}\nType:{3}\nNeedItemID:{4}\nNeedItemCount:{5}\nGetItemIDs:{6}\nGetItemCount:{7}", ID, Name, IsNeedLearn, Type, NeedItemIDs, NeedItemCount, GetItemIDs, GetItemCount);
        return text;
    }
}
