using UnityEngine;

namespace BlockEnhancementMod;

public class CogMotoControllerHinge_GenericEnhanceScript : ChangeSpeedBlock
{
	private CogMotorControllerHinge cmcl;

	public override void SafeAwake()
	{
		try
		{
			cmcl = ((Component)this).GetComponent<CogMotorControllerHinge>();
			if ((Object)(object)cmcl != (Object)null)
			{
				base.SpeedSlider = cmcl.SpeedSlider;
				base.EnableChangeSpeed = true;
			}
			else
			{
				base.EnableChangeSpeed = false;
			}
		}
		catch
		{
		}
		base.SafeAwake();
	}
}
