using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthShow : MonoBehaviour {

    private Slider m_HealthSlider;
    private NpcController m_nc;
	// Use this for initialization
	void Start ()
    {
        m_HealthSlider = GetComponent<Slider>();
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_nc ==null)
        {
            m_nc = transform.parent.parent.GetComponent<NpcController>();
        }
        else if (m_nc.NpcProp!=null)
        {
            float showValue = (Mathf.Round((m_nc.currentHealth*100 / m_nc.NpcProp.MaxHealth)) / 100);
            m_HealthSlider.value = showValue;
        }
        transform.LookAt(Camera.main.transform.position);
    }
}
