using UnityEngine;

namespace BlockEnhancementMod;

internal class SpringCode_GenericEnhanceScript : ChangeSpeedBlock
{
	internal SpringCode springCode;

	public override void SafeAwake()
	{
		springCode = ((Component)this).GetComponent<SpringCode>();
		base.SpeedSlider = springCode.SpeedSlider;
		base.SafeAwake();
	}
}
