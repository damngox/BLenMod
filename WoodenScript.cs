using UnityEngine;

namespace BlockEnhancementMod;

internal class WoodenScript : ChangeHardnessBlock
{
	public override void SafeAwake()
	{
		base.HardnessMenu = AddMenu("Hardness", 1, SingleInstance<LanguageManager>.Instance.CurrentLanguage.WoodenHardness);
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			base.ConfigurableJoint = ((Component)this).GetComponent<ConfigurableJoint>();
			hardness.SwitchWoodHardness(base.HardnessMenu.Value, base.ConfigurableJoint);
		}
	}
}
