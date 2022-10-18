using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class CountryCodePicker : MonoBehaviour
{
	[Header("Show dropdown result")]
	public Text Text_DropdownValue;

	[SerializeField] private TextAsset m_Json_Code;
	[SerializeField] private TextAsset m_Json_Flag;
	[SerializeField] private Texture2D m_Sprite_Flag;

	private bool m_IsInit;
	private Dropdown m_Dropdown;
	private List<CountryInfoClass> m_CountryInfoList = new List<CountryInfoClass>();
	private readonly string m_DefaultOption = "Taiwan";

	public static CountryCodePicker Instance;

	void Awake()
	{
		Init();
	}

	private void OnDestroy()
	{
		if (m_Dropdown != null)
		{
			m_Dropdown.onValueChanged.RemoveListener(OnSelectItem);
		}
	}

	private void Init()
	{
		if (m_IsInit) { return; }

		if (Instance == null) { Instance = this; }
		m_Dropdown = GetComponent<Dropdown>();
		m_Dropdown.onValueChanged.AddListener(OnSelectItem);
		InitCountryInfoData(m_Sprite_Flag, m_Json_Code, m_Json_Flag);
		InitDropdownList();
		m_IsInit = true;
	}

	private void InitCountryInfoData(Texture2D iTexture, TextAsset iCodeData, TextAsset iFlagData)
	{
		if (iTexture  == null)  { return; }
		if (iFlagData == null || string.IsNullOrEmpty(iFlagData.text)) { return; }
		if (iCodeData == null || string.IsNullOrEmpty(iCodeData.text)) { return; }

		CountryCodeInfoList aCodeInfo = JsonUtility.FromJson<CountryCodeInfoList>(iCodeData.text);
		CountryFlagInfoList aFlagInfo = JsonUtility.FromJson<CountryFlagInfoList>(iFlagData.text);

		m_CountryInfoList.Clear();
		FlagInfo aCountryFlagInfo;
		for (int i = 0; i < aCodeInfo.data.Count; i++)
		{
			aCountryFlagInfo = aFlagInfo.data.Find(x => x.name == aCodeInfo.data[i].abbr);
			if (aCountryFlagInfo == null) { continue; }
			m_CountryInfoList.Add(new CountryInfoClass(){
				Name = aCodeInfo.data[i].name,
				Abbr = aCodeInfo.data[i].abbr,
				Code = aCodeInfo.data[i].code,
				Flag = Sprite.Create(m_Sprite_Flag, new Rect(-aCountryFlagInfo.x, aCountryFlagInfo.y + 360, 24, 24), new Vector2(0.5f, 0.5f))
			});
		}
	}

	private void InitDropdownList()
	{
		m_Dropdown.ClearOptions();

		if (m_CountryInfoList == null) { return; }
		List<Dropdown.OptionData> aDropdownItemLsit = new List<Dropdown.OptionData>();
		for (int i = 0; i < m_CountryInfoList.Count; i++)
		{
			aDropdownItemLsit.Add(new Dropdown.OptionData(m_CountryInfoList[i].Name, m_CountryInfoList[i].Flag));
		}
		m_Dropdown.AddOptions(aDropdownItemLsit);

		int aDefaultOption = m_Dropdown.options.FindIndex(x => x.text == m_DefaultOption);
		m_Dropdown.value = aDefaultOption;
	}

	private void OnSelectItem(int iValue)
	{
		if (Text_DropdownValue.text == null) { return; }
		// Text_DropdownValue.text = "+" + m_CountryInfoList[iValue].Code.ToString();
		// m_Dropdown
	}

	public CountryInfoClass GetCountryInfo(string iCountryName)
	{
		return m_CountryInfoList.Find(x => x.Name == iCountryName);
	}
}

[Serializable]
public class CountryFlagInfoList
{
	public List<FlagInfo> data;
}

[Serializable]
public class FlagInfo
{
	public string name;
	public int x;
	public int y;
}

[Serializable]
public class CountryCodeInfoList
{
	public List<CodeInfo> data;
}

[Serializable]
public class CodeInfo
{
	public string name;
	public List<string> area;
	public string abbr;
	public int code;
}

[Serializable]
public class CountryInfoClass
{
	public string Name;
	public string Abbr;
	public int Code;
	public Sprite Flag;
}
