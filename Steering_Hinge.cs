using UnityEngine;

namespace BlockEnhancementMod;

internal class Steering_Hinge : SteeringWheel_GenericEnhanceScript
{
	private MKey rtcEnableKey;

	public override void SafeAwake()
	{
		base.SafeAwake();
		rtcEnableKey = ((SaveableDataHolder)base.BB).AddKey("RTC Enable", "rtc_enable", (KeyCode)114);
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)rtcEnableKey).DisplayInMapper = value;
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		base.SimulateUpdateAlways_EnhancementEnable();
		if (rtcEnableKey.IsPressed || rtcEnableKey.EmulationPressed())
		{
			steeringWheel.ReturnToCenterToggle.IsActive = !steeringWheel.ReturnToCenterToggle.IsActive;
		}
	}
}
