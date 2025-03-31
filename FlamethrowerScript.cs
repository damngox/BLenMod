using UnityEngine;

namespace BlockEnhancementMod;

internal class FlamethrowerScript : ChangeSpeedBlock
{
	private FlamethrowerController flamethrowerController;

	private MSlider thrustForceSlider;

	private MColourSlider flameColorSlider;

	public string FlameShader = "Particles/Additive";

	private Rigidbody rigidbody;

	public override void SafeAwake()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		thrustForceSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ThrustForce, "Thrust Force", 0f, 0f, 5f);
		flameColorSlider = AddColourSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.FlameColor, "Flame Color", Color.white, snapColors: false);
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)thrustForceSlider).DisplayInMapper = value;
		((MapperType)flameColorSlider).DisplayInMapper = value;
		base.DisplayInMapper(value);
	}

	public override void OnSimulateStartAlways()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.EnhancementEnabled)
		{
			flamethrowerController = ((Component)this).GetComponent<FlamethrowerController>();
			rigidbody = ((Component)this).GetComponent<Rigidbody>();
			base.SpeedSlider = thrustForceSlider;
			((Renderer)((Component)flamethrowerController.fireParticles).GetComponent<ParticleSystemRenderer>()).material.shader = Shader.Find(FlameShader);
			flamethrowerController.fireParticles.startColor = flameColorSlider.Value;
		}
	}

	public override void SimulateFixedUpdate_EnhancementEnabled()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (!StatMaster.isClient && thrustForceSlider.Value != 0f && flamethrowerController.isFlaming)
		{
			rigidbody.AddRelativeForce(-Vector3.forward * thrustForceSlider.Value * 100f);
		}
	}
}
