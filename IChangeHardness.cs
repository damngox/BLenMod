using UnityEngine;

namespace BlockEnhancementMod;

public interface IChangeHardness
{
	ConfigurableJoint ConfigurableJoint { get; }

	MMenu HardnessMenu { get; }
}
