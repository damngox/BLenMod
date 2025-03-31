using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace BlockEnhancementMod;

internal class ArmorScript : EnhancementBlock
{
	private Camera watchCamera;

	private GameObject cameraObject;

	private GameObject screenObject;

	private int channelIndex;

	private List<string> channelList = new List<string> { "-1" };

	private MeshRenderer mr;

	private RenderTexture rt;

	private FixedCameraController fcc;

	private MMenu channelMenu;

	private MValue widthPixelValue;

	private MValue heightPixelValue;

	private MKey changeChannelKey;

	private MKey switchKey;

	public override void SafeAwake()
	{
		changeChannelKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ChangeChannel, "Change Channel", (KeyCode)99);
		switchKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Switch, "Switch", (KeyCode)101);
		channelMenu = AddMenu("Channel Menu", 0, channelList);
		widthPixelValue = AddValue(SingleInstance<LanguageManager>.Instance.CurrentLanguage.WidthPixel, "Width", 800f);
		heightPixelValue = AddValue(SingleInstance<LanguageManager>.Instance.CurrentLanguage.HeightPixel, "Height", 800f);
		((MonoBehaviour)this).StartCoroutine(initChannelInBuilding());
		Events.OnBlockPlaced += RefreshCameraChannelList;
		Events.OnBlockRemoved += RefreshCameraChannelList;
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		((MapperType)channelMenu).DisplayInMapper = value;
		((MapperType)changeChannelKey).DisplayInMapper = value;
		((MapperType)widthPixelValue).DisplayInMapper = value;
		((MapperType)heightPixelValue).DisplayInMapper = value;
		((MapperType)switchKey).DisplayInMapper = value;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		base.OnSimulateStart_EnhancementEnabled();
		channelList = ((Component)base.BB.BuildingBlock).GetComponent<ArmorScript>().channelList;
		channelIndex = ((Component)base.BB.BuildingBlock).GetComponent<ArmorScript>().channelIndex;
		fcc = Object.FindObjectOfType<FixedCameraController>();
		if (channelIndex >= 0 && !((Object)(object)fcc == (Object)null))
		{
			rt = new RenderTexture(Mathf.Clamp((int)widthPixelValue.Value, 0, 1920), Mathf.Clamp((int)heightPixelValue.Value, 0, 1080), 0);
			cameraObject = new GameObject("WatchCamera");
			cameraObject.transform.SetParent(((Component)this).transform);
			watchCamera = cameraObject.AddComponent<Camera>();
			watchCamera.CopyFrom(Camera.main);
			watchCamera.targetTexture = rt;
			screenObject = GameObject.CreatePrimitive((PrimitiveType)4);
			((Object)screenObject).name = "Screen";
			Object.Destroy((Object)(object)screenObject.GetComponent<MeshCollider>());
			screenObject.transform.SetParent(((Component)this).transform);
			screenObject.transform.position = ((Component)this).transform.position;
			screenObject.transform.rotation = ((Component)this).transform.rotation;
			screenObject.transform.localPosition = Vector3.forward * 0.25f;
			screenObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			screenObject.transform.localScale = Vector3.one * 0.07f;
			mr = ((Component)screenObject.transform).GetComponent<MeshRenderer>();
			((Renderer)mr).material.shader = Shader.Find("Particles/Alpha Blended");
			((Renderer)mr).material.mainTexture = (Texture)(object)rt;
			((Renderer)mr).sortingOrder = 50;
			stickToCamera(channelIndex);
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		base.SimulateUpdateAlways_EnhancementEnable();
		if (channelIndex < 0)
		{
			return;
		}
		if (changeChannelKey.IsPressed || changeChannelKey.EmulationPressed())
		{
			if (++channelIndex > channelList.Count - 1)
			{
				channelIndex = 0;
			}
			stickToCamera(channelIndex);
		}
		if (switchKey.IsPressed || switchKey.EmulationPressed())
		{
			((Renderer)mr).enabled = !((Renderer)mr).enabled;
		}
	}

	private void stickToCamera(int index)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)fcc != (Object)null)
		{
			index = Mathf.Clamp(index, 0, fcc.cameras.Count - 1);
			FixedCameraBlock val = fcc.cameras[index];
			Transform compoundTracker = val.CompoundTracker;
			cameraObject.transform.SetParent(val.CompositeTracker);
			cameraObject.transform.position = ((Component)compoundTracker).transform.position;
			cameraObject.transform.rotation = ((Component)compoundTracker).transform.rotation;
			cameraObject.transform.eulerAngles = compoundTracker.eulerAngles;
		}
	}

	public void RefreshCameraChannelList(Block block)
	{
		try
		{
			((MonoBehaviour)this).StartCoroutine(waitThreeFrame());
		}
		catch
		{
		}
		void refreshChannelList()
		{
			if (!Machine.Active().isSimulating)
			{
				channelList = new List<string>();
				int num = -1;
				foreach (BlockBehaviour buildingBlock in Machine.Active().BuildingBlocks)
				{
					if (((Object)buildingBlock).name == "CameraBlock")
					{
						num++;
						channelList.Add(num.ToString());
					}
				}
				if (num < 0)
				{
					channelList = SingleInstance<LanguageManager>.Instance.CurrentLanguage.NullChannelList;
					channelIndex = -1;
				}
				else if (channelIndex < 0)
				{
					channelIndex = 0;
				}
				channelMenu.Items = channelList;
				if (channelIndex > channelList.Count + 1)
				{
					int num3 = (channelMenu.Value = 0);
					channelIndex = num3;
				}
				else if (channelIndex < 0)
				{
					channelMenu.Value = 0;
				}
				else
				{
					channelMenu.Value = channelIndex;
				}
			}
		}
		IEnumerator waitThreeFrame()
		{
			for (int i = 0; i < 3; i++)
			{
				yield return 0;
			}
			refreshChannelList();
		}
	}

	private IEnumerator initChannelInBuilding()
	{
		if (Machine.Active().isSimulating)
		{
			yield break;
		}
		int defaultChannelIndex = 0;
		try
		{
			defaultChannelIndex = SingleInstance<EnhancementBlockController>.Instance.PMI.Blocks.ToList().Find((BlockInfo match) => match.Guid == base.BB.Guid).Data.ReadInt("bmt-Channel Menu");
		}
		catch
		{
		}
		for (int i = 0; i < 10; i++)
		{
			yield return 0;
		}
		RefreshCameraChannelList(null);
		if (channelList != null)
		{
			if (defaultChannelIndex < channelList.Count)
			{
				channelMenu.Value = (channelIndex = defaultChannelIndex);
			}
			yield break;
		}
		channelList = SingleInstance<LanguageManager>.Instance.CurrentLanguage.NullChannelList;
		MMenu obj2 = channelMenu;
		ArmorScript armorScript = this;
		int value = 0;
		armorScript.channelIndex = 0;
		obj2.Value = value;
	}
}
