using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour, IPointerDownHandler
{

    private CharacterProperty charProp;

    private Image headFrame;
    private Image headImage;
    private Image HeadImage
    {
        get
        {
            if (headImage == null)
            {
                headImage = transform.GetChild(0).GetComponent<Image>();
            }
            return headImage;
        }
    }

    void Start()
    {
        headFrame = transform.GetComponent<Image>();
        headFrame.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //TODO 如果角色属性不为空，传递给管理，并加亮框提示
        //TODO 如果属性为空，没有反应
        if (charProp == null)
            return;
        transform.parent.SendMessage("CheckFrame");
        transform.parent.parent.parent.parent.SendMessage("ChangeMessage", charProp);
        headFrame.enabled = true; 
        //Debug.Log(charProp.Name);
    }

    public void CloseFrame()
    {
        headFrame.enabled = false;
    }

    public void SetCharProp(CharacterProperty _charProp)
    {
        if (_charProp != null)
        {
            charProp = _charProp;
            HeadImage.sprite = Resources.Load<Sprite>(_charProp.Sprite);
        }
    }
}