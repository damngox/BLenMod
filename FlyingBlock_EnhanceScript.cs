using UnityEngine;

namespace BlockEnhancementMod;

internal class FlyingBlock_EnhanceScript : ChangeSpeedBlock
{
	private FlyingController flyingController;

	public override void SafeAwake()
	{
		flyingController = ((Component)this).GetComponent<FlyingController>();
		base.SpeedSlider = flyingController.SpeedSlider;
		base.SafeAwake();
	}
}
