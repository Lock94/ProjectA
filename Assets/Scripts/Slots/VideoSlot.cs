using UnityEngine;
using UnityEngine.UI;

public class VideoSlot : MonoBehaviour
{
    public string VideoName;

    private Text ImageText;
    private Button MinusValue;
    private Button AddValue;
    private int currentValue = 4;//0-最低 1-低 2-中 3-高4-最高

    private void Start()
    {
        MinusValue = transform.Find("Minus").GetComponent<Button>();
        AddValue = transform.Find("Add").GetComponent<Button>();
        ImageText = transform.Find("Image").transform.GetChild(0).GetComponent<Text>();

        MinusValue.onClick.AddListener(delegate { MinusSlotValue(); });
        AddValue.onClick.AddListener(delegate { AddSlotValue(); });
    }

    void MinusSlotValue()
    {
        currentValue--;
        AddValue.interactable = true;
        if (currentValue <= 0)
        {
            currentValue = 0;
            MinusValue.interactable = false;
        }
        else
        {
            MinusValue.interactable = true;
        }
        ImageText.text = valueToText(currentValue);
    }
    void AddSlotValue()
    {
        currentValue++;
        MinusValue.interactable = true;
        if (currentValue >= 4)
        {
            currentValue = 4;
            AddValue.interactable = false;
        }
        else
        {
            AddValue.interactable = true;
        }
        ImageText.text = valueToText(currentValue);
    }

    string valueToText(int value)
    {
        string text = " ";
        switch (value)
        {
            case 0:
                text = "最低";
                break;
            case 1:
                text = "低";
                break;
            case 2:
                text = "中";
                break;
            case 3:
                text = "高";
                break;
            case 4:
                text = "最高";
                break;
            default:
                break;
        }
        return text;
    }
}