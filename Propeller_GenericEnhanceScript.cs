using UnityEngine;

namespace BlockEnhancementMod;

internal class Propeller_GenericEnhanceScript : ChangeHardnessBlock
{
	private MKey SwitchKey;

	private MToggle EffectToggle;

	private MToggle ToggleToggle;

	private MToggle LiftIndicatorToggle;

	private LineRenderer LR;

	private AxialDrag AD;

	private Vector3 liftVector;

	private Vector3 axisDragOrgin;

	public override void SafeAwake()
	{
		SwitchKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Enabled, "Switch", (KeyCode)111);
		base.HardnessMenu = AddMenu("Hardness", 1, SingleInstance<LanguageManager>.Instance.CurrentLanguage.WoodenHardness);
		EffectToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.EnabledOnAwake, "Effect", defaultValue: true);
		ToggleToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ToggleMode, "Toggle Mode", defaultValue: true);
		LiftIndicatorToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.LiftIndicator, "Lift Indicator", defaultValue: false);
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)SwitchKey).DisplayInMapper = value;
		((MapperType)EffectToggle).DisplayInMapper = value;
		((MapperType)ToggleToggle).DisplayInMapper = value;
		((MapperType)LiftIndicatorToggle).DisplayInMapper = value;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		base.ConfigurableJoint = ((Component)this).GetComponent<ConfigurableJoint>();
		AD = ((Component)this).GetComponent<AxialDrag>();
		axisDragOrgin = AD.AxisDrag;
		SetVelocityCap(EffectToggle.IsActive);
		hardness.SwitchWoodHardness(base.HardnessMenu.Value, base.ConfigurableJoint);
		initLineRenderer();
		if (LiftIndicatorToggle.IsActive)
		{
			((Renderer)LR).enabled = true;
		}
		void initLineRenderer()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			GameObject val = new GameObject("Lift Indicator");
			val.transform.SetParent(((Component)this).transform);
			LR = val.GetComponent<LineRenderer>() ?? val.AddComponent<LineRenderer>();
			LR.useWorldSpace = true;
			LR.SetVertexCount(2);
			((Renderer)LR).material = new Material(Shader.Find("Particles/Additive"));
			LR.SetColors(Color.red, Color.yellow);
			LR.SetWidth(0.5f, 0.5f);
			((Renderer)LR).enabled = false;
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		if (StatMaster.isClient)
		{
			return;
		}
		if (SwitchKey.IsPressed || SwitchKey.EmulationPressed())
		{
			EffectToggle.IsActive = !EffectToggle.IsActive;
			SetVelocityCap(EffectToggle.IsActive);
		}
		if (!ToggleToggle.IsActive && (SwitchKey.IsReleased || SwitchKey.EmulationReleased()))
		{
			EffectToggle.IsActive = !EffectToggle.IsActive;
			SetVelocityCap(EffectToggle.IsActive);
		}
		if (LiftIndicatorToggle.IsActive)
		{
			if ((Object)(object)base.ConfigurableJoint != (Object)null)
			{
				liftVector = ((Component)((BasicInfo)AD).Rigidbody).transform.TransformVector(AD.xyz * AD.currentVelocitySqr);
				LR.SetPosition(0, ((Component)this).transform.TransformPoint(((BasicInfo)AD).Rigidbody.centerOfMass));
				LR.SetPosition(1, ((Component)this).transform.TransformPoint(((BasicInfo)AD).Rigidbody.centerOfMass) + liftVector);
			}
			else
			{
				((Renderer)LR).enabled = false;
			}
		}
	}

	private void SetVelocityCap(bool value)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		AD.AxisDrag = ((!value) ? Vector3.zero : axisDragOrgin);
	}
}
