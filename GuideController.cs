using System.Collections;
using UnityEngine;

namespace BlockEnhancementMod;

internal class GuideController : MonoBehaviour
{
	private Rigidbody parentRigidbody;

	private BlockBehaviour parentBlock;

	private RadarScript blockRadar;

	private float sourceSpeedPower;

	public float searchAngle;

	public float torque;

	public float maxTorque = 1500f;

	private Vector3 previousPosition = Vector3.zero;

	private Transform preTargetTransform;

	public static float pFactor = BlockEnhancementMod.ModSetting.GuideControl_P_Factor;

	public static float iFactor = BlockEnhancementMod.ModSetting.GuideControl_I_Factor;

	public static float dFactor = BlockEnhancementMod.ModSetting.GuideControl_D_Factor;

	public float integral;

	public float lastError;

	private readonly float aeroEffectMultiplier = 5f;

	private Vector3 aeroEffectPosition = Vector3.zero;

	public bool enableAerodynamicEffect;

	public bool constantForce;

	private Vector3 ForwardDirection
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			if (parentBlock.BlockID != 59)
			{
				return ((Component)parentBlock).transform.forward;
			}
			return ((Component)parentBlock).transform.up;
		}
	}

	public bool Switch { get; set; }

	public void Setup(BlockBehaviour sourceBlock, Rigidbody sourceRigidbody, RadarScript sourceRadar, float sourceSpeedPower, float sourceSearchAngle, float sourceTorque, bool constantForce)
	{
		parentBlock = sourceBlock;
		parentRigidbody = sourceRigidbody;
		blockRadar = sourceRadar;
		this.sourceSpeedPower = sourceSpeedPower;
		enableAerodynamicEffect = false;
		searchAngle = sourceSearchAngle;
		torque = sourceTorque;
		this.constantForce = constantForce;
	}

	private void FixedUpdate()
	{
		if (!StatMaster.isClient && !((Object)(object)parentBlock == (Object)null) && !((Object)(object)parentRigidbody == (Object)null) && Switch)
		{
			if ((Object)(object)blockRadar != (Object)null)
			{
				_ = blockRadar;
				/*Error near IL_0042: Invalid metadata token*/;
			}
			if (enableAerodynamicEffect)
			{
				((MonoBehaviour)this).StartCoroutine(AddAerodynamicsToRocketVelocity());
			}
		}
	}

	private IEnumerator AddGuideForce()
	{
		previousPosition = Vector3.zero;
		integral = 0f;
		lastError = 0f;
		_ = blockRadar;
		/*Error near IL_0041: Invalid metadata token*/;
		yield break;
	}

	private IEnumerator AddAerodynamicsToRocketVelocity()
	{
		aeroEffectPosition = ForwardDirection * 5f;
		Vector3 val = ((Component)this).transform.InverseTransformDirection(parentRigidbody.velocity);
		Vector3 val2 = new Vector3(0.1f, 0f, 0.1f) * aeroEffectMultiplier;
		Vector3 velocity = parentRigidbody.velocity;
		float num = Mathf.Min(((Vector3)(ref velocity)).sqrMagnitude, 30f);
		Vector3 val3 = Vector4.op_Implicit(((Component)this).transform.localToWorldMatrix * Vector4.op_Implicit(Vector3.Scale(val2, -val)) * num);
		parentRigidbody.AddForceAtPosition(val3, ((BasicInfo)parentBlock).CenterOfBounds - aeroEffectPosition);
		parentRigidbody.AddForceAtPosition(-0.1f * val3, ((BasicInfo)parentBlock).CenterOfBounds + aeroEffectPosition);
		yield break;
	}
}
