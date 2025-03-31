using System;
using UnityEngine;

namespace BlockEnhancementMod;

[Obsolete]
internal class SpinningScript : EnhancementBlock
{
	private MKey RotationKey;

	private MToggle LockedToggle;

	private MSlider LerpSlider;

	private bool Locked;

	private float Lerp = 12f;

	private HingeJoint HJ;

	private CogMotorControllerHinge CMCH;

	public override void SafeAwake()
	{
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)RotationKey).DisplayInMapper = value;
		((MapperType)LockedToggle).DisplayInMapper = value;
		((MapperType)LerpSlider).DisplayInMapper = value;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		HJ = ((Component)this).GetComponent<HingeJoint>();
		CMCH = ((Component)this).GetComponent<CogMotorControllerHinge>();
		CMCH.speedLerpSmooth = Lerp;
		if (Locked)
		{
			HJ.useLimits = true;
		}
		else
		{
			HJ.useLimits = false;
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		MotorFreezeRotation(RotationKey.IsHeld);
	}

	private void MotorFreezeRotation(bool value)
	{
		if (value)
		{
			HJ.useMotor = false;
			((Component)this).GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)64;
		}
		else
		{
			((Component)this).GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
			HJ.useMotor = true;
		}
	}
}
