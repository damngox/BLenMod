using System;
using UnityEngine;

namespace BlockEnhancementMod;

[Obsolete]
internal class SteeringHinge : SteeringWheel_GenericEnhanceScript
{
	private new SteeringWheel steeringWheel;

	private MToggle r2cToggle;

	private MToggle NearToggle;

	private MKey addSpeedKey;

	private MKey reduceSpeedKey;

	public bool ReturnToCenter;

	public bool Near = true;

	private MSlider rotationSpeedSlider;

	private Rigidbody rigidbody;

	private MKey leftKey;

	private MKey rightKey;

	private MKey lastKey;

	public override void SafeAwake()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		steeringWheel = ((Component)this).GetComponent<SteeringWheel>();
		r2cToggle = ((SaveableDataHolder)base.BB).AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ReturnToCenter, "Return to center", ReturnToCenter);
		r2cToggle.Toggled += (ToggleHandler)delegate(bool value)
		{
			bool returnToCenter = (((MapperType)NearToggle).DisplayInMapper = value);
			ReturnToCenter = returnToCenter;
			ChangedProperties((MapperType)(object)r2cToggle);
		};
		NearToggle = ((SaveableDataHolder)base.BB).AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Near, "Near", Near);
		NearToggle.Toggled += (ToggleHandler)delegate(bool value)
		{
			Near = value;
			ChangedProperties((MapperType)(object)NearToggle);
		};
		leftKey = ((SaveableDataHolder)steeringWheel).KeyList.Find((MKey match) => ((MapperType)match).Key == "left");
		rightKey = ((SaveableDataHolder)steeringWheel).KeyList.Find((MKey match) => ((MapperType)match).Key == "right");
		rotationSpeedSlider = steeringWheel.SpeedSlider;
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)r2cToggle).DisplayInMapper = value;
		((MapperType)NearToggle).DisplayInMapper = value && ReturnToCenter;
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			rigidbody = ((Component)this).GetComponent<Rigidbody>();
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		if (StatMaster.isClient)
		{
			return;
		}
		getLastKey();
		if (!leftKey.IsHeld && !rightKey.IsHeld && ReturnToCenter && steeringWheel.AngleToBe != 0f)
		{
			rigidbody.WakeUp();
			float num = Time.deltaTime * 100f * steeringWheel.targetAngleSpeed * rotationSpeedSlider.Value;
			float num2 = 0f;
			if (!Near && lastKey != null)
			{
				float num3 = Mathf.Sign(steeringWheel.AngleToBe);
				num2 = ((((MapperType)lastKey).Key == "left" && num3 < 0f) ? 179f : ((!(((MapperType)lastKey).Key == "right") || !(num3 > 0f)) ? 0f : (-179f)));
			}
			else
			{
				num2 = 0f;
			}
			steeringWheel.AngleToBe = Mathf.MoveTowardsAngle(steeringWheel.AngleToBe, num2, num);
		}
		void getLastKey()
		{
			if (steeringWheel.AngleToBe != 0f)
			{
				if (leftKey.IsReleased)
				{
					lastKey = leftKey;
				}
				if (rightKey.IsReleased)
				{
					lastKey = rightKey;
				}
			}
			else
			{
				lastKey = null;
			}
		}
	}
}
