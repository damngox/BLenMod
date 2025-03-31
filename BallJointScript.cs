using UnityEngine;

namespace BlockEnhancementMod;

public class BallJointScript : EnhancementBlock
{
	public MToggle RotationToggle;

	private ConfigurableJoint CJ;

	public override void SafeAwake()
	{
		RotationToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.CvJoint, "Rotation", defaultValue: false);
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)RotationToggle).DisplayInMapper = value;
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			CJ = ((Component)this).GetComponent<ConfigurableJoint>();
			if (RotationToggle.IsActive)
			{
				CJ.angularYMotion = (ConfigurableJointMotion)0;
				((Joint)CJ).breakTorque = float.PositiveInfinity;
			}
		}
	}
}
