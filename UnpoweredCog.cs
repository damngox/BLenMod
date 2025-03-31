using UnityEngine;

namespace BlockEnhancementMod;

internal class UnpoweredCog : EnhancementBlock
{
	private MKey SwitchKey;

	private MToggle HeldToggle;

	public bool Switch;

	private ConfigurableJoint hinge;

	private bool state;

	private bool lastState;

	public override void SafeAwake()
	{
		SwitchKey = AddKey("Switch", "Switch", (KeyCode)114);
		SwitchKey.InvokeKeysChanged();
		HeldToggle = AddToggle("Toggle", "Toggle", defaultValue: false);
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			hinge = ((Component)this).GetComponent<ConfigurableJoint>();
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (StatMaster.isClient)
		{
			return;
		}
		Debug.Log((object)hinge.targetPosition);
		Debug.Log((object)hinge.targetRotation);
		if (SwitchKey.IsPressed || SwitchKey.EmulationPressed())
		{
			state = !state;
			Debug.Log((object)"switch");
		}
		if (HeldToggle.IsActive && SwitchKey.IsReleased)
		{
			state = !state;
		}
		if (state != lastState)
		{
			lastState = state;
			if (state)
			{
				MakeCogStatic();
			}
			else
			{
				MakeCogNormal();
			}
		}
	}

	private void MakeCogNormal()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		SoftJointLimitSpring angularXLimitSpring = hinge.angularXLimitSpring;
		((SoftJointLimitSpring)(ref angularXLimitSpring)).damper = 0f;
		hinge.angularXLimitSpring = angularXLimitSpring;
		JointDrive angularXDrive = hinge.angularXDrive;
		((JointDrive)(ref angularXDrive)).positionSpring = 0f;
		hinge.angularXDrive = angularXDrive;
	}

	private void MakeCogStatic()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		SoftJointLimitSpring angularXLimitSpring = hinge.angularXLimitSpring;
		((SoftJointLimitSpring)(ref angularXLimitSpring)).damper = 10000f;
		hinge.angularXLimitSpring = angularXLimitSpring;
		JointDrive angularXDrive = hinge.angularXDrive;
		((JointDrive)(ref angularXDrive)).positionSpring = 10000f;
		hinge.angularXDrive = angularXDrive;
	}
}
