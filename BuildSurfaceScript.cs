using System;
using System.Collections;
using UnityEngine;

namespace BlockEnhancementMod;

internal class BuildSurfaceScript : EnhancementBlock
{
	private MToggle kinematicToggle;

	private MToggle transparentSupportToggle;

	private MSlider radiusSlider;

	private BuildSurface buildSurface;

	[SerializeField]
	private MeshRenderer materialRenderer;

	[SerializeField]
	private Material transparentMaterial;

	private Material baseMaterial;

	[SerializeField]
	private bool isWood;

	[SerializeField]
	private bool isDefaultSkin;

	[SerializeField]
	private int visualValue;

	private int lastSkinValue;

	private Shader oldShader;

	[SerializeField]
	private bool transparentUsed;

	public override void SafeAwake()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		base.SafeAwake();
		buildSurface = ((Component)this).GetComponent<BuildSurface>();
		kinematicToggle = AddToggle("Kinematic", "kinematic", defaultValue: false);
		transparentSupportToggle = AddToggle("Transparent Support", "Transparent", defaultValue: false);
		transparentSupportToggle.Toggled += new ToggleHandler(transparentChanged);
		radiusSlider = AddSlider("Joint Radius", "Radius", 1f, 0.1f, 1f);
		radiusSlider.ValueChanged += new ValueChangeHandler(radiusChanged);
		((MonoBehaviour)this).StartCoroutine(wait());
		IEnumerator wait()
		{
			yield return (object)new WaitUntil((Func<bool>)(() => base.BB.Visual != null));
			((BlockBehaviour)buildSurface).Visual.ValueChanged += new VisChangeHandler(skinChanged);
			materialRenderer = ((Component)((Component)this).transform.FindChild("Vis")).GetComponent<MeshRenderer>();
		}
	}

	private void judgeDispaly_transparentToggle()
	{
		bool flag = (((int)Mathf.Clamp01((float)visualValue) != 0) ? true : false);
		((MapperType)transparentSupportToggle).DisplayInMapper = base.EnhancementEnabled && flag && ((BlockBehaviour)buildSurface).Visual != null;
	}

	public override void DisplayInMapper(bool enhance)
	{
		MToggle obj = kinematicToggle;
		bool displayInMapper = (((MapperType)radiusSlider).DisplayInMapper = enhance);
		((MapperType)obj).DisplayInMapper = displayInMapper;
		judgeDispaly_transparentToggle();
	}

	public override void BuildingUpdateAlways_EnhancementEnabled()
	{
		base.BuildingUpdateAlways_EnhancementEnabled();
		if (lastSkinValue != visualValue)
		{
			judgeDispaly_transparentToggle();
			lastSkinValue = visualValue;
		}
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		base.OnSimulateStart_EnhancementEnabled();
		buildSurface = ((Component)this).GetComponent<BuildSurface>();
		if (kinematicToggle.IsActive)
		{
			((MonoBehaviour)this).StartCoroutine(wait1());
		}
		bool isActive = transparentSupportToggle.IsActive;
		Debug.Log((object)("trans: " + isActive));
		if (isActive)
		{
			transparentChanged(isActive);
		}
		IEnumerator wait1()
		{
			for (int i = 0; i < 3; i++)
			{
				yield return 0;
			}
			((BasicInfo)buildSurface).Rigidbody.isKinematic = true;
			((Component)buildSurface).gameObject.isStatic = true;
		}
	}

	private void colorChanged(float value)
	{
		Debug.Log((object)"??");
	}

	private void radiusChanged(float value)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		TriggerSetJointSurface[] jointTriggers = buildSurface.JointTriggers;
		for (int i = 0; i < jointTriggers.Length; i++)
		{
			((Component)jointTriggers[i]).transform.localScale = Vector3.one * value;
		}
		BoxCollider[] addingPoints = buildSurface.AddingPoints;
		for (int i = 0; i < addingPoints.Length; i++)
		{
			((Component)addingPoints[i]).transform.localScale = Vector3.one * value;
		}
	}

	private void transparentChanged(bool value)
	{
		Debug.Log((object)(((object)((BlockBehaviour)buildSurface).Visual)?.ToString() + "|" + visualValue));
		if (OptionsMaster.skinsEnabled && visualValue != 0)
		{
			Debug.Log((object)("trans changed: " + value));
			if (value)
			{
				((Renderer)materialRenderer).material = transparentMaterial;
			}
			else
			{
				((Renderer)materialRenderer).material = baseMaterial;
			}
		}
	}

	private void skinChanged(int value)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		if (base.EnhancementEnabled)
		{
			Debug.Log((object)("skin changed" + value));
			visualValue = value;
			Debug.Log((object)(lastSkinValue + "||" + visualValue));
			if (visualValue != lastSkinValue)
			{
				transparentSupportToggle.IsActive = false;
			}
			if (visualValue != 0 && visualValue != lastSkinValue)
			{
				Object.Destroy((Object)(object)baseMaterial);
				Object.Destroy((Object)(object)transparentMaterial);
				baseMaterial = new Material(((Renderer)materialRenderer).material);
				transparentMaterial = intiMaterial(((Renderer)materialRenderer).material);
			}
		}
		static Material intiMaterial(Material material)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			return new Material(material)
			{
				name = "Transparent Material",
				shader = Shader.Find("Transparent/Diffuse")
			};
		}
	}
}
