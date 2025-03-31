using UnityEngine;

namespace BlockEnhancementMod;

internal class SteeringWheel_GenericEnhanceScript : ChangeSpeedBlock
{
	internal SteeringWheel steeringWheel;

	public override void SafeAwake()
	{
		steeringWheel = ((Component)this).GetComponent<SteeringWheel>();
		if ((Object)(object)steeringWheel != (Object)null)
		{
			base.SpeedSlider = steeringWheel.SpeedSlider;
		}
		base.SafeAwake();
	}
}
