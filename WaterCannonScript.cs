using UnityEngine;

namespace BlockEnhancementMod;

internal class WaterCannonScript : ChangeSpeedBlock
{
	private MToggle BoilingToggle;

	private WaterCannonController WCC;

	private BlockVisualController BVC;

	private FireTag FT;

	public override void SafeAwake()
	{
		BoilingToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Boiling, "Boiling", defaultValue: false);
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)BoilingToggle).DisplayInMapper = value;
		base.DisplayInMapper(value);
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			WCC = ((Component)this).GetComponent<WaterCannonController>();
			BVC = ((Component)this).GetComponent<BlockVisualController>();
			FT = ((Component)this).GetComponent<FireTag>();
			base.SpeedSlider = WCC.StrengthSlider;
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		base.SimulateUpdateAlways_EnhancementEnable();
		if (!StatMaster.isClient && BoilingToggle.IsActive && WCC.isActive)
		{
			WCC.OnIgnite(FT, ((Component)base.BB).GetComponentsInChildren<Collider>()[0], true);
		}
	}
}
