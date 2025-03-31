using System;
using System.Collections;
using Modding;
using UnityEngine;

namespace BlockEnhancementMod.Blocks;

internal class WheelScript : CogMotoControllerHinge_GenericEnhanceScript
{
	private struct PSaF
	{
		public Vector3 Position;

		public Vector3 Scale;

		public float Friction;

		public float Bounciness;

		public static PSaF one = new PSaF
		{
			Position = Vector3.one,
			Scale = Vector3.one,
			Friction = 1f
		};

		public static PSaF GetPositionScaleAndFriction(int id)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			PSaF result = default(PSaF);
			switch (id)
			{
			case 2:
				result.Position = new Vector3(0f, 0f, 0.175f);
				result.Scale = new Vector3(0.98f, 0.98f, 1.75f);
				result.Friction = 0.6f;
				result.Bounciness = 0f;
				return result;
			case 46:
				result.Position = new Vector3(0f, 0f, 0.45f);
				result.Scale = new Vector3(1.38f, 1.38f, 3.75f);
				result.Friction = 0.8f;
				result.Bounciness = 0f;
				return result;
			case 40:
				result.Position = new Vector3(0f, 0f, 0.175f);
				result.Scale = new Vector3(0.98f, 0.98f, 1.75f);
				result.Friction = 1f;
				result.Bounciness = 0f;
				return result;
			case 60:
				result.Position = new Vector3(0f, 0f, 0.45f);
				result.Scale = new Vector3(1.38f, 1.38f, 1.75f);
				result.Friction = 1f;
				result.Bounciness = 0f;
				return result;
			default:
				return one;
			}
		}
	}

	private MToggle collisionToggle;

	private MToggle CustomColliderToggle;

	private MToggle ShowColliderToggle;

	private MSlider FrictionSlider;

	private MSlider BouncinessSlider;

	private float Friction = 0.8f;

	private int ID;

	private static GameObject WheelColliderOrgin;

	private Collider[] Colliders;

	private MeshFilter mFilter;

	private MeshRenderer mRenderer;

	private MeshCollider mCollider;

	private PhysicMaterial wheelPhysicMaterialOrgin;

	public GameObject WheelCollider;

	public override void SafeAwake()
	{
		ID = ((Component)this).GetComponent<BlockVisualController>().ID;
		Friction = PSaF.GetPositionScaleAndFriction(ID).Friction;
		collisionToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Collision, "Collision", defaultValue: true);
		CustomColliderToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.CustomCollider, "Custom Collider", defaultValue: false);
		ShowColliderToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ShowCollider, "Show Collider", defaultValue: true);
		FrictionSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Friction, "Friction", Friction, 0.1f, 3f);
		BouncinessSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Bounciness, "Bounciness", 0f, 0f, 1f);
		if ((Object)(object)WheelColliderOrgin == (Object)null)
		{
			((MonoBehaviour)this).StartCoroutine(ReadWheelMesh());
		}
		base.SafeAwake();
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)collisionToggle).DisplayInMapper = value;
		((MapperType)CustomColliderToggle).DisplayInMapper = value;
		((MapperType)ShowColliderToggle).DisplayInMapper = value && CustomColliderToggle.IsActive;
		((MapperType)FrictionSlider).DisplayInMapper = value;
		((MapperType)BouncinessSlider).DisplayInMapper = value;
	}

	private static bool IsWheel(int id)
	{
		bool flag = false;
		return id switch
		{
			46 => true, 
			2 => true, 
			40 => true, 
			60 => true, 
			_ => false, 
		};
	}

	public override void OnSimulateStartAlways()
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		if (!base.EnhancementEnabled)
		{
			return;
		}
		Colliders = ((Component)this).GetComponentsInChildren<Collider>();
		wheelPhysicMaterialOrgin = Colliders[0].material;
		PhysicMaterial material = SetPhysicMaterial(FrictionSlider.Value, BouncinessSlider.Value, (PhysicMaterialCombine)0);
		if (CustomColliderToggle.IsActive)
		{
			if ((Object)(object)WheelCollider != (Object)null)
			{
				return;
			}
			Collider[] colliders = Colliders;
			foreach (Collider val in colliders)
			{
				if (((Object)val).name == "CubeColliders" || ((Object)val).name == "Collider")
				{
					((Collider)(BoxCollider)val).isTrigger = true;
				}
			}
			WheelCollider = (GameObject)Object.Instantiate((Object)(object)WheelColliderOrgin, ((Component)this).transform.position, ((Component)this).transform.rotation, ((Component)this).transform);
			WheelCollider.SetActive(true);
			((Object)WheelCollider).name = "Wheel Collider";
			WheelCollider.transform.SetParent(((Component)this).transform);
			mCollider = WheelCollider.GetComponent<MeshCollider>();
			mCollider.convex = true;
			((Collider)mCollider).material = material;
			base.BB.myBounds.childColliders.Add((Collider)(object)mCollider);
			if (ShowColliderToggle.IsActive)
			{
				mRenderer = WheelCollider.AddComponent<MeshRenderer>();
				((Renderer)mRenderer).material.color = Color.red;
			}
			PSaF positionScaleAndFriction = PSaF.GetPositionScaleAndFriction(ID);
			WheelCollider.transform.parent = ((Component)this).transform;
			WheelCollider.transform.rotation = ((Component)this).transform.rotation;
			WheelCollider.transform.position = ((Component)this).transform.TransformPoint(((Component)this).transform.InverseTransformPoint(((Component)this).transform.position) + positionScaleAndFriction.Position);
			WheelCollider.transform.localScale = positionScaleAndFriction.Scale;
		}
		else
		{
			Collider[] colliders = Colliders;
			foreach (Collider val2 in colliders)
			{
				if (((Object)val2).name == "CubeColliders")
				{
					((Collider)((Component)val2).GetComponent<BoxCollider>()).material = material;
				}
			}
		}
		if (collisionToggle.IsActive)
		{
			return;
		}
		foreach (Collider childCollider in base.BB.myBounds.childColliders)
		{
			childCollider.isTrigger = true;
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		base.SimulateUpdateAlways_EnhancementEnable();
		CogMotorControllerHinge component = ((Component)base.BB).GetComponent<CogMotorControllerHinge>();
		HingeJoint component2 = ((Component)base.BB).GetComponent<HingeJoint>();
		if (!((Object)(object)component == (Object)null) && !component.AutoBreakToggle.IsActive)
		{
			if (component.Input == 0f)
			{
				component2.useMotor = false;
			}
			else
			{
				component2.useMotor = true;
			}
		}
	}

	public override void SimulateFixedUpdate_EnhancementEnabled()
	{
		base.SimulateFixedUpdate_EnhancementEnabled();
		((BasicInfo)base.BB).Rigidbody.WakeUp();
	}

	private static PhysicMaterial SetPhysicMaterial(float friction, float bounciness, PhysicMaterialCombine combine)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		return new PhysicMaterial
		{
			staticFriction = friction,
			dynamicFriction = friction,
			bounciness = bounciness,
			frictionCombine = combine,
			bounceCombine = (PhysicMaterialCombine)0
		};
	}

	private static PhysicMaterial SetPhysicMaterial(PSaF pSaF)
	{
		return SetPhysicMaterial(pSaF.Friction, pSaF.Bounciness, (PhysicMaterialCombine)3);
	}

	private static IEnumerator ReadWheelMesh()
	{
		WheelColliderOrgin = new GameObject("Wheel Collider Orgin");
		WheelColliderOrgin.transform.SetParent(((Component)SingleInstance<EnhancementBlockController>.Instance).transform);
		ModMesh modMesh = ModResource.CreateMeshResource("Wheel Mesh", "Resources/Wheel.obj", false);
		Mesh wMesh = SingleInstance<AssetManager>.Instance.LoadFormObj2("Wheel Mesh", "Resources/Wheel.obj");
		yield return (object)new WaitUntil((Func<bool>)(() => ((ModResource)modMesh).Available));
		WheelColliderOrgin.AddComponent<MeshFilter>().sharedMesh = wMesh;
		MeshCollider obj = WheelColliderOrgin.AddComponent<MeshCollider>();
		obj.sharedMesh = wMesh;
		obj.convex = true;
		WheelColliderOrgin.SetActive(false);
	}
}
