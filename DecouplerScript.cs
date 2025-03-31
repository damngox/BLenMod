using UnityEngine;

namespace BlockEnhancementMod.Blocks;

internal class DecouplerScript : EnhancementBlock
{
	private MSlider ExplodeForceSlider;

	private MSlider ExplodeTorqueSlider;

	private ExplosiveBolt EB;

	public override void SafeAwake()
	{
		ExplodeForceSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ExplodeForce, "ExplodeForce", 1000f, 0f, 3000f);
		ExplodeTorqueSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ExplodeTorque, "ExplodeTorque", 2000f, 0f, 2500f);
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)ExplodeForceSlider).DisplayInMapper = value;
		((MapperType)ExplodeTorqueSlider).DisplayInMapper = value;
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			EB = ((Component)this).GetComponent<ExplosiveBolt>();
			EB.explodePower = ExplodeForceSlider.Value;
			EB.explodeTorquePower = ExplodeTorqueSlider.Value;
		}
	}
}
