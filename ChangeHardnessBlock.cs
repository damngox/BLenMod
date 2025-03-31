using UnityEngine;

namespace BlockEnhancementMod;

public class ChangeHardnessBlock : EnhancementBlock, IChangeHardness
{
	private enum Material
	{
		LowCarbonSteel = 0,
		MedianSoftWood = 0,
		MidCarbonSteel = 1,
		HighCarbonSteel = 2,
		SoftWood = 3,
		HardWood = 4,
		VeryHardWood = 5,
		None = 6
	}

	public struct Hardness
	{
		private enum Material
		{
			LowCarbonSteel = 0,
			MedianSoftWood = 0,
			MidCarbonSteel = 1,
			HighCarbonSteel = 2,
			SoftWood = 3,
			HardWood = 4,
			VeryHardWood = 5,
			None = 6
		}

		public JointProjectionMode projectionMode;

		public float projectionAngle;

		public float projectionDistance;

		public Hardness(JointProjectionMode mode, float angle, float distance)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			projectionMode = mode;
			projectionAngle = angle;
			projectionDistance = distance;
		}

		public Hardness(ConfigurableJoint joint)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			projectionMode = joint.projectionMode;
			projectionAngle = joint.projectionAngle;
			projectionDistance = joint.projectionDistance;
		}

		public ConfigurableJoint toConfigurableJoint(ConfigurableJoint joint)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			joint.projectionMode = projectionMode;
			joint.projectionAngle = projectionAngle;
			joint.projectionDistance = projectionDistance;
			return joint;
		}

		public Hardness SwitchHardness(int index)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			Hardness result = default(Hardness);
			switch (index)
			{
			case 1:
				result.projectionMode = (JointProjectionMode)1;
				result.projectionAngle = 0.5f;
				break;
			case 2:
				result.projectionMode = (JointProjectionMode)1;
				result.projectionAngle = 0f;
				break;
			case 3:
				result.projectionMode = (JointProjectionMode)1;
				result.projectionAngle = 10f;
				result.projectionDistance = 5f;
				break;
			case 4:
				result.projectionMode = (JointProjectionMode)1;
				result.projectionAngle = 5f;
				result.projectionDistance = 2.5f;
				break;
			case 5:
				result.projectionMode = (JointProjectionMode)1;
				result.projectionAngle = 0f;
				result.projectionDistance = 0f;
				break;
			default:
				result.projectionMode = (JointProjectionMode)0;
				break;
			}
			return result;
		}

		public Hardness GetOrginHardness(ConfigurableJoint joint)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return new Hardness(joint.projectionMode, joint.projectionAngle, joint.projectionDistance);
		}

		public ConfigurableJoint SwitchWoodHardness(int index, ConfigurableJoint joint)
		{
			return (ConfigurableJoint)(index switch
			{
				0 => SwitchHardness(index + 3).toConfigurableJoint(joint), 
				2 => SwitchHardness(index + 2).toConfigurableJoint(joint), 
				3 => SwitchHardness(index + 2).toConfigurableJoint(joint), 
				_ => GetOrginHardness(joint).toConfigurableJoint(joint), 
			});
		}

		public ConfigurableJoint SwitchMetalHardness(int index, ConfigurableJoint joint)
		{
			return (ConfigurableJoint)(index switch
			{
				1 => SwitchHardness(index).toConfigurableJoint(joint), 
				2 => SwitchHardness(index).toConfigurableJoint(joint), 
				_ => GetOrginHardness(joint).toConfigurableJoint(joint), 
			});
		}
	}

	internal Hardness hardness;

	public ConfigurableJoint ConfigurableJoint { get; set; }

	public MMenu HardnessMenu { get; set; }

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)HardnessMenu).DisplayInMapper = value;
	}
}
