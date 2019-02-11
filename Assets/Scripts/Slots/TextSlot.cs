using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlot : MonoBehaviour
{
    private Text title;
    private Text value;

    private Text Title
    {
        get
        {
            if (title == null)
            {
                title = transform.GetChild(0).GetComponent<Text>();
            }
            return title;
        }
    }
    private Text Value
    {
        get
        {
            if (value ==null)
            {
                value = transform.GetChild(1).GetComponent<Text>();
            }
            return value;
        }
    }

    public void SetTextSlot(string _title,string _value )
    {
        Title.text = _title;
        Value.text = _value;
    }
}
