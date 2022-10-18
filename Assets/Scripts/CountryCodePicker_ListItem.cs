using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryCodePicker_ListItem : MonoBehaviour
{
	[SerializeField] private Image m_Image_Flag;
	[SerializeField] private Text m_Text_Name;
	[SerializeField] private Text m_Text_Code;

	private string m_CountryName;

	private void Start()
	{
		if (m_Text_Name != null)
		{
			m_CountryName = m_Text_Name.text;
		}

		if (CountryCodePicker.Instance)
		{
			CountryInfoClass aCountryInfo = CountryCodePicker.Instance.GetCountryInfo(m_CountryName);
			if (m_Text_Code != null)
			{
				m_Text_Code.text = "+" + aCountryInfo.Code;
			}

			if (m_Image_Flag != null)
			{
				m_Image_Flag.enabled = false;
				m_Image_Flag.sprite = aCountryInfo.Flag;
				m_Image_Flag.enabled = true;
			}
		}
	}
}
