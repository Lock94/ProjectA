using UnityEngine;
using UnityEngine.UI;

public class SliderSlot : MonoBehaviour
{
    public string ValueName;
    private Text Title;
    private Button MinusValue;
    private Button AddValue;

    private Slider SlotValue;


    void Start()
    {
        Title = transform.Find("Title").GetComponent<Text>();

        MinusValue = transform.Find("Minus").GetComponent<Button>();
        AddValue = transform.Find("Add").GetComponent<Button>();

        SlotValue = transform.Find("Slider").GetComponent<Slider>();

        MinusValue.onClick.AddListener(delegate { MinusSlotValue(); });
        AddValue.onClick.AddListener(delegate { AddSlotValue(); });
    }

    private void MinusSlotValue()
    {
        SlotValue.value -= 0.1f;
        AddValue.interactable = true;
        if (SlotValue.value <= 0)
        {
            SlotValue.value = 0;
            MinusValue.interactable = false;
        }
        else
        {
            MinusValue.interactable = true;
        }
        if (transform.parent.GetComponent<SettingCommon>())
        {
            transform.parent.GetComponent<SettingCommon>().ChangeValue(ValueName, SlotValue.value);
        }
        else if (transform.parent.parent.GetComponent<SettingInput>())
        {
            transform.parent.parent.GetComponent<SettingInput>().ChangeValue(ValueName, SlotValue.value);
        }

    }

    private void AddSlotValue()
    {
        SlotValue.value += 0.1f;
        MinusValue.interactable = true;
        if (SlotValue.value >= 1)
        {
            SlotValue.value = 1;
            AddValue.interactable = false;
        }
        else
        {
            AddValue.interactable = true;
        }
        if (transform.parent.GetComponent<SettingCommon>())
        {
            transform.parent.GetComponent<SettingCommon>().ChangeValue(ValueName, SlotValue.value);
        }
        else if (transform.parent.parent.GetComponent<SettingInput>())
        {
            transform.parent.parent.GetComponent<SettingInput>().ChangeValue(ValueName, SlotValue.value);
        }
    }
}