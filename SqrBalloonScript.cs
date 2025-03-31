using UnityEngine;

namespace BlockEnhancementMod;

internal class SqrBalloonScript : ChangeSpeedBlock
{
	private MKey effectKey;

	private MToggle toggleToggle;

	private MToggle dragTogetherToggle;

	private SqrBalloonController sqrBalloonController;

	private bool effected;

	private float lastBuoyancy;

	private float defaultDrag;

	private float defaultAngularDrag;

	public bool Effected
	{
		get
		{
			return effected;
		}
		set
		{
			effected = value;
		}
	}

	public override void SafeAwake()
	{
		effectKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Effected, "Effect", (KeyCode)101);
		toggleToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ToggleMode, "Toggle", defaultValue: true);
		dragTogetherToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.DragTogether, "Together", defaultValue: true);
		sqrBalloonController = ((Component)this).GetComponent<SqrBalloonController>();
		base.SpeedSlider = sqrBalloonController.PowerSlider;
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)effectKey).DisplayInMapper = value;
		((MapperType)toggleToggle).DisplayInMapper = value;
		((MapperType)dragTogetherToggle).DisplayInMapper = value;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		base.OnSimulateStart_EnhancementEnabled();
		lastBuoyancy = base.Speed;
		defaultDrag = ((Component)this).GetComponent<Rigidbody>().drag;
		defaultAngularDrag = ((Component)this).GetComponent<Rigidbody>().angularDrag;
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		try
		{
			if (!effected)
			{
				if (base.AddSpeedKey.IsPressed || base.AddSpeedKey.EmulationPressed())
				{
					base.Speed += base.ChangeSpeedValue.Value;
					lastBuoyancy = base.Speed;
				}
				if (base.ReduceSpeedKey.IsPressed || base.ReduceSpeedKey.EmulationPressed())
				{
					base.Speed -= base.ChangeSpeedValue.Value;
					lastBuoyancy = base.Speed;
				}
			}
		}
		catch
		{
		}
		if (toggleToggle.IsActive)
		{
			if (effectKey.IsPressed || effectKey.EmulationPressed())
			{
				effected = !effected;
				setValue();
			}
		}
		else
		{
			if (effectKey.IsHeld || effectKey.EmulationHeld(false))
			{
				effected = true;
			}
			else
			{
				effected = false;
			}
			setValue();
		}
		void setValue()
		{
			base.Speed = (effected ? 0f : lastBuoyancy);
			if (dragTogetherToggle.IsActive)
			{
				Rigidbody component = ((Component)this).GetComponent<Rigidbody>();
				component.drag = (effected ? 0f : defaultDrag);
				component.angularDrag = (effected ? 0f : defaultAngularDrag);
			}
		}
	}
}
