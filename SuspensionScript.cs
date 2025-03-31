using UnityEngine;

namespace BlockEnhancementMod;

public class SuspensionScript : ChangeSpeedBlock, IChangeHardness
{
	private MKey ExtendKey;

	private MKey ShrinkKey;

	private MToggle HydraulicToggle;

	private MToggle R2CToggle;

	private MSlider FeedSlider;

	private MSlider ExtendLimitSlider;

	private MSlider ShrinkLimitSlider;

	private Rigidbody RB;

	public MMenu HardnessMenu { get; private set; }

	public ConfigurableJoint ConfigurableJoint { get; private set; }

	public override void SafeAwake()
	{
		HardnessMenu = AddMenu("Hardness", 0, SingleInstance<LanguageManager>.Instance.CurrentLanguage.MetalHardness);
		ExtendKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Extend, "Extend", (KeyCode)101);
		ShrinkKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Retract, "Shrink", (KeyCode)102);
		HydraulicToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.HydraulicMode, "Pressure", defaultValue: false);
		R2CToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ReturnToCenter, "Return to center", defaultValue: false);
		FeedSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.FeedSpeed, "feed", 0.5f, 0f, 2f);
		ExtendLimitSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ExtendLimit, "ExtendLimit", 1f, 0f, 3f);
		ShrinkLimitSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.RetractLimit, "ShrinkLimit", 1f, 0f, 3f);
		base.SpeedSlider = FeedSlider;
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		bool flag = value && HydraulicToggle.IsActive;
		base.DisplayInMapper(flag);
		((MapperType)HardnessMenu).DisplayInMapper = value;
		((MapperType)ExtendKey).DisplayInMapper = flag;
		((MapperType)ShrinkKey).DisplayInMapper = flag;
		((MapperType)HydraulicToggle).DisplayInMapper = value;
		((MapperType)R2CToggle).DisplayInMapper = flag;
		((MapperType)FeedSlider).DisplayInMapper = flag;
		((MapperType)ExtendLimitSlider).DisplayInMapper = flag;
		((MapperType)ShrinkLimitSlider).DisplayInMapper = flag;
	}

	public override void OnSimulateStartAlways()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (base.EnhancementEnabled)
		{
			ConfigurableJoint = ((Component)this).GetComponent<ConfigurableJoint>();
			RB = ((Component)this).GetComponent<Rigidbody>();
			ChangeHardnessBlock.Hardness hardness = new ChangeHardnessBlock.Hardness(ConfigurableJoint);
			float limit = Mathf.Max(ExtendLimitSlider.Value, ShrinkLimitSlider.Value);
			SoftJointLimit linearLimit = ConfigurableJoint.linearLimit;
			((SoftJointLimit)(ref linearLimit)).limit = limit;
			ConfigurableJoint.linearLimit = linearLimit;
			JointDrive xDrive = ConfigurableJoint.xDrive;
			ConfigurableJoint.xDrive = xDrive;
			hardness.SwitchMetalHardness(HardnessMenu.Value, ConfigurableJoint);
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		base.SimulateUpdateAlways_EnhancementEnable();
	}

	public override void SimulateFixedUpdate_EnhancementEnabled()
	{
		base.SimulateFixedUpdate_EnhancementEnabled();
		float? target2;
		if (!StatMaster.isClient && HydraulicToggle.IsActive)
		{
			target2 = null;
			CalculationTarget();
			if (target2.HasValue)
			{
				SuspensionMoveTowards(target2.Value, FeedSlider.Value * (ConfigurableJoint.swapBodies ? (-1f) : 1f));
			}
		}
		void CalculationTarget()
		{
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			bool flag = false;
			if (ExtendKey.IsHeld || ExtendKey.EmulationHeld(false))
			{
				flag = true;
				target2 = 0f - ExtendLimitSlider.Value;
			}
			if (ShrinkKey.IsHeld || ShrinkKey.EmulationHeld(false))
			{
				flag = true;
				target2 = ShrinkLimitSlider.Value;
			}
			if (R2CToggle.IsActive && !flag && ConfigurableJoint.targetPosition != Vector3.zero)
			{
				target2 = 0f;
			}
		}
		void SuspensionMoveTowards(float target, float feed, float delta = 0.005f)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			RB.WakeUp();
			ConfigurableJoint.targetPosition = Vector3.MoveTowards(ConfigurableJoint.targetPosition, new Vector3(target, 0f, 0f), feed * delta);
		}
	}
}
