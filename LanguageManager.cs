using System;
using System.Collections.Generic;
using Localisation;

namespace BlockEnhancementMod;

public class LanguageManager : SingleInstance<LanguageManager>
{
	public Action<string> OnLanguageChanged;

	private string currentLanguageName;

	private string lastLanguageName = "English";

	private Dictionary<string, ILanguage> Dic_Language = new Dictionary<string, ILanguage>
	{
		{
			"简体中文",
			new Chinese()
		},
		{
			"English",
			new English()
		},
		{
			"日本語",
			new Japanese()
		}
	};

	public override string Name { get; } = "Language Manager";

	public ILanguage CurrentLanguage { get; private set; } = new English();

	private void Awake()
	{
		OnLanguageChanged = (Action<string>)Delegate.Combine(OnLanguageChanged, new Action<string>(ChangLanguage));
	}

	private void Update()
	{
		currentLanguageName = SingleInstance<LocalisationManager>.Instance.currLangName;
		if (!lastLanguageName.Equals(currentLanguageName))
		{
			lastLanguageName = currentLanguageName;
			OnLanguageChanged(currentLanguageName);
		}
	}

	private void ChangLanguage(string value)
	{
		try
		{
			CurrentLanguage = Dic_Language[value];
		}
		catch
		{
			CurrentLanguage = Dic_Language["English"];
		}
	}
}
