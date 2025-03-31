using UnityEngine;

namespace BlockEnhancementMod;

internal class HingeScript : EnhancementBlock
{
	private MToggle springToggle;

	private MSlider springSlider;

	private MSlider damperSlider;

	private ConfigurableJoint hingeJoint;

	public override void SafeAwake()
	{
		base.SafeAwake();
		springToggle = AddToggle("Spring Toggle", "spring toggle", defaultValue: false);
		springSlider = AddSlider("Spring", "spring", 1f, 0f, 10f);
		damperSlider = AddSlider("Damper", "damper", 0f, 0f, 10f);
	}

	public override void DisplayInMapper(bool enhance)
	{
		base.DisplayInMapper(enhance);
		((MapperType)springToggle).DisplayInMapper = enhance;
		MSlider obj = springSlider;
		bool displayInMapper = (((MapperType)damperSlider).DisplayInMapper = enhance & springToggle.IsActive);
		((MapperType)obj).DisplayInMapper = displayInMapper;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		base.OnSimulateStart_EnhancementEnabled();
		hingeJoint = ((Component)this).GetComponent<ConfigurableJoint>();
		if (springToggle.IsActive)
		{
			JointDrive angularXDrive = hingeJoint.angularXDrive;
			((JointDrive)(ref angularXDrive)).positionSpring = springSlider.Value * 1000f;
			((JointDrive)(ref angularXDrive)).positionDamper = damperSlider.Value * 1000f;
			hingeJoint.angularXDrive = angularXDrive;
		}
	}
}
