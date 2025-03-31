using UnityEngine;

namespace BlockEnhancementMod.Blocks;

internal class PistonScript : ChangeSpeedBlock, IChangeHardness
{
	private MSlider DamperSlider;

	private MSlider LimitSlider;

	private SliderCompress SC;

	public MMenu HardnessMenu { get; private set; }

	public ConfigurableJoint ConfigurableJoint { get; private set; }

	public override void SafeAwake()
	{
		HardnessMenu = AddMenu("Hardness", 0, SingleInstance<LanguageManager>.Instance.CurrentLanguage.MetalHardness);
		DamperSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Damper, "Damper", 1f, 0f, 5f);
		LimitSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Limit, "Limit", 1.1f, 0f, 1.1f);
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)HardnessMenu).DisplayInMapper = value;
		((MapperType)DamperSlider).DisplayInMapper = value;
		((MapperType)LimitSlider).DisplayInMapper = value;
		base.DisplayInMapper(value);
	}

	public override void OnSimulateStartAlways()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (base.EnhancementEnabled)
		{
			SC = ((Component)this).GetComponent<SliderCompress>();
			ConfigurableJoint = ((Component)this).GetComponent<ConfigurableJoint>();
			ChangeHardnessBlock.Hardness hardness = new ChangeHardnessBlock.Hardness(ConfigurableJoint);
			base.SpeedSlider = SC.SpeedSlider;
			SC.newLimit = LimitSlider.Value * (float)FlipToSign(((BlockBehaviour)SC).Flipped);
			JointDrive xDrive = ConfigurableJoint.xDrive;
			((JointDrive)(ref xDrive)).positionDamper = ((JointDrive)(ref xDrive)).positionDamper * DamperSlider.Value;
			ConfigurableJoint.xDrive = xDrive;
			hardness.SwitchMetalHardness(HardnessMenu.Value, ConfigurableJoint);
		}
		static int FlipToSign(bool value)
		{
			if (!value)
			{
				return -1;
			}
			return 1;
		}
	}
}
