using UnityEngine;

namespace BlockEnhancementMod;

public class SmallwheelScript : EnhancementBlock
{
	private MSlider SpeedSlider;

	private SmallWheel SW;

	public override void SafeAwake()
	{
		SpeedSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.RotatingSpeed, "Speed", 5f, 0f, 5f);
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)SpeedSlider).DisplayInMapper = value;
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			SW = ((Component)this).GetComponent<SmallWheel>();
			SW.speed = SpeedSlider.Value;
		}
	}
}
