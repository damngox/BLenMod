using UnityEngine;

namespace BlockEnhancementMod;

public class ChangeSpeedBlock : EnhancementBlock, IChangeSpeed
{
	public float Speed
	{
		get
		{
			return SpeedSlider.Value;
		}
		set
		{
			SpeedSlider.Value = value;
		}
	}

	public bool EnableChangeSpeed { get; internal set; } = true;

	public MSlider SpeedSlider { get; set; }

	public MKey AddSpeedKey { get; set; }

	public MKey ReduceSpeedKey { get; set; }

	public MValue ChangeSpeedValue { get; set; }

	public override void SafeAwake()
	{
		base.SafeAwake();
		if (EnableChangeSpeed)
		{
			AddSpeedKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.AddSpeed, "Add Speed", (KeyCode)61);
			ReduceSpeedKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ReduceSpeed, "Reduce Speed", (KeyCode)45);
			ChangeSpeedValue = AddValue(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ChangeSpeed, "Change Speed", 0.1f);
		}
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		try
		{
			MKey addSpeedKey = AddSpeedKey;
			MKey reduceSpeedKey = ReduceSpeedKey;
			bool flag2 = (((MapperType)ChangeSpeedValue).DisplayInMapper = value && EnableChangeSpeed);
			bool displayInMapper = (((MapperType)reduceSpeedKey).DisplayInMapper = flag2);
			((MapperType)addSpeedKey).DisplayInMapper = displayInMapper;
		}
		catch
		{
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		base.SimulateUpdateAlways_EnhancementEnable();
		try
		{
			if (EnableChangeSpeed)
			{
				if (AddSpeedKey.IsPressed || AddSpeedKey.EmulationPressed())
				{
					Speed += ChangeSpeedValue.Value;
				}
				if (ReduceSpeedKey.IsPressed || ReduceSpeedKey.EmulationPressed())
				{
					Speed -= ChangeSpeedValue.Value;
				}
			}
		}
		catch
		{
		}
	}
}
