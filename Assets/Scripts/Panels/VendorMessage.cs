using UnityEngine;
using UnityEngine.UI;

public class VendorMessage : MonoBehaviour
{
    private Text m_yinHangRate;
    private Text m_chaoPiaoRate;
    private Text m_level;
    private Text m_yinHangAmount;
    private Text m_chaoPiaoAmount;
    private Button m_yinHangUse;
    private Button m_chaoPiaoUse;
    private Button m_makeSure;
    private Button m_reset;

    public Sprite YinUse;
    public Sprite YinUnUse;
    public Sprite ChaoUse;
    public Sprite ChaoUnUse;

    private void Start()
    {
        m_yinHangRate = transform.Find("YinHangRate").GetComponent<Text>();
        m_chaoPiaoRate = transform.Find("ChaoPiaoRate").GetComponent<Text>();
        m_level = transform.Find("Level").GetComponent<Text>();
        m_yinHangAmount = transform.Find("YinHangAmount").GetComponent<Text>();
        m_chaoPiaoAmount = transform.Find("ChaoPiaoAmount").GetComponent<Text>();
        m_yinHangUse = transform.Find("YinHangUse").GetComponent<Button>();
        m_chaoPiaoUse = transform.Find("ChaoPiaoUse").GetComponent<Button>();
        m_makeSure = transform.Find("MakeSure").GetComponent<Button>();
        m_reset = transform.Find("Reset").GetComponent<Button>();

        m_yinHangUse.onClick.AddListener(delegate { OnUseYin(); });
        m_chaoPiaoUse.onClick.AddListener(delegate { OnUseChao(); });
        m_reset.onClick.AddListener(delegate { OnReset(); });
        m_makeSure.onClick.AddListener(delegate { OnMakeSure(); });

        SetButtonUse(0);
        OnUseYin();
    }

    public void SetMessage(float yinRate,float chaoRate,string level,int yinAmount,int chaoAmount)
    {
        m_yinHangRate.text = string.Format("银行券：   1:{0}", yinRate);
        m_chaoPiaoRate.text = string.Format("清宝钞：   1:{0}", chaoRate);
        m_level.text = level;
        m_yinHangAmount.text = yinAmount.ToString();
        m_chaoPiaoAmount.text = chaoAmount.ToString();
    }

    public void SetYinAmount(int yinAmount,int yinAmountBef)
    {
        string text = yinAmountBef.ToString();
        if (yinAmountBef> yinAmount)
        {
            text += string.Format("<color=red>(-{0})</color>", yinAmountBef - yinAmount);
        }
        else if(yinAmountBef < yinAmount)
        {
            text += string.Format("<color=green>(+{0})</color>", yinAmount - yinAmountBef);
        }
        m_yinHangAmount.text = text;
    }

    public void SetChaoAmount(int chaoAmount,int chaAmountBef)
    {
        string text = chaAmountBef.ToString();
        if (chaAmountBef > chaoAmount)
        {
            text += string.Format("<color=red>(-{0})</color>", chaAmountBef - chaoAmount);
        }
        else if(chaAmountBef < chaoAmount)
        {
            text += string.Format("<color=green>(+{0})</color>", chaoAmount - chaAmountBef);
        }
        m_chaoPiaoAmount.text = text;
    }
    /// <summary>
    /// 确认使用的钞票
    /// </summary>
    /// <param name="id">1是银行券，2是清宝钞</param>
    public void SetButtonUse(int id)
    {
        if (id == 1)
        {
            m_yinHangUse.transform.GetComponent<Image>().sprite = YinUse;
            m_chaoPiaoUse.transform.GetComponent<Image>().sprite = ChaoUnUse;
        }
        else if (id == 2)
        {
            m_yinHangUse.transform.GetComponent<Image>().sprite = YinUnUse;
            m_chaoPiaoUse.transform.GetComponent<Image>().sprite = ChaoUse;
        }
        transform.parent.GetComponent<VendorBigPanel>().UseCoinID = id;
    }

    public void OnUseYin()
    {
        transform.parent.SendMessage("SetPrice", 1);
        SetButtonUse(1);
    }

    public void OnUseChao()
    {
        transform.parent.SendMessage("SetPrice", 2);
        SetButtonUse(2);
    }
    public void OnReset()
    {
        transform.parent.SendMessage("ResetItem");
    }
    public void OnMakeSure()
    {
        transform.parent.SendMessage("Hide");
    }
}