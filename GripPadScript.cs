using UnityEngine;

namespace BlockEnhancementMod;

public class GripPadScript : ChangeSpeedBlock, IChangeHardness
{
	private MSlider FrictionSlider;

	private Collider[] colliders;

	public MMenu HardnessMenu { get; private set; }

	public ConfigurableJoint ConfigurableJoint { get; private set; }

	public override void SafeAwake()
	{
		HardnessMenu = AddMenu("Hardness", 1, SingleInstance<LanguageManager>.Instance.CurrentLanguage.WoodenHardness);
		FrictionSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Friction, "Friction", 1f, 0f, 5f);
		base.SpeedSlider = FrictionSlider;
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)HardnessMenu).DisplayInMapper = value;
		((MapperType)FrictionSlider).DisplayInMapper = value;
		base.DisplayInMapper(value);
	}

	public override void OnSimulateStartAlways()
	{
		if (!base.EnhancementEnabled)
		{
			return;
		}
		colliders = ((Component)this).GetComponentsInChildren<Collider>();
		ConfigurableJoint = ((Component)this).GetComponent<ConfigurableJoint>();
		ChangeHardnessBlock.Hardness hardness = new ChangeHardnessBlock.Hardness(ConfigurableJoint);
		Collider[] array = colliders;
		foreach (Collider val in array)
		{
			if (((Object)val).name == "Collider")
			{
				PhysicMaterial material = val.material;
				float staticFriction = (val.material.dynamicFriction = FrictionSlider.Value * 1000f);
				material.staticFriction = staticFriction;
				break;
			}
		}
		hardness.SwitchWoodHardness(HardnessMenu.Value, ConfigurableJoint);
	}
}
