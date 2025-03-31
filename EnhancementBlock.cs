using System;
using System.Collections;
using System.Collections.Generic;
using Modding.Blocks;
using UnityEngine;

namespace BlockEnhancementMod;

public class EnhancementBlock : MonoBehaviour, ILimitsDisplay
{
	public static bool EnhanceMore = BlockEnhancementMod.ModSetting.EnhanceMore;

	public MToggle EnhancementToggle;

	[Obsolete]
	public Action<XDataHolder> BlockDataLoadEvent;

	[Obsolete]
	public Action<XDataHolder> BlockDataSaveEvent;

	public Action<MapperType> PropertiseChangedEvent;

	private bool isFirstFrame = true;

	private bool mapperMe;

	public BlockBehaviour BB { get; internal set; }

	public bool EnhancementEnabled { get; set; }

	private void Awake()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		BB = ((Component)this).GetComponent<BlockBehaviour>();
		SafeAwake();
		EnhancementToggle = ((SaveableDataHolder)BB).AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Enhancement, "Enhancement", EnhancementEnabled);
		EnhancementToggle.Toggled += (ToggleHandler)delegate(bool value)
		{
			EnhancementEnabled = value;
			PropertiseChangedEvent((MapperType)(object)EnhancementToggle);
		};
		PropertiseChangedEvent = (Action<MapperType>)Delegate.Combine(PropertiseChangedEvent, new Action<MapperType>(ChangedProperties));
		PropertiseChangedEvent = (Action<MapperType>)Delegate.Combine(PropertiseChangedEvent, (Action<MapperType>)delegate
		{
			DisplayInMapper(EnhancementEnabled);
		});
		PropertiseChangedEvent?.Invoke((MapperType)(object)EnhancementToggle);
		((MonoBehaviour)this).StartCoroutine(onPlaced());
		IEnumerator onPlaced()
		{
			yield return (object)new WaitUntil((Func<bool>)(() => BB.PlacementComplete));
			if (!((BasicInfo)BB).ParentMachine.isSimulating)
			{
				OnPlaced();
			}
		}
	}

	private void Update()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		if (((BasicInfo)BB).SimPhysics)
		{
			if (isFirstFrame)
			{
				isFirstFrame = false;
				if (EnhancementEnabled)
				{
					OnSimulateStart_EnhancementEnabled();
				}
				if (!StatMaster.isClient)
				{
					OnSimulateStartClient();
				}
				OnSimulateStartAlways();
			}
			SimulateUpdateAlways();
			if (EnhancementEnabled)
			{
				if (StatMaster.isHosting)
				{
					SimulateUpdateHost_EnhancementEnabled();
				}
				if (StatMaster.isClient)
				{
					SimulateUpdateClient_EnhancementEnabled();
				}
				SimulateUpdateAlways_EnhancementEnable();
			}
			return;
		}
		if (EnhancementEnabled)
		{
			BuildingUpdateAlways_EnhancementEnabled();
		}
		if (BlockMapper.IsOpen && (Object)(object)BB == (Object)(object)BlockMapper.CurrentInstance.Block)
		{
			if (InputManager.CopyKeys())
			{
				OnCopy();
			}
			if (InputManager.PasteKeys())
			{
				OnPaste();
			}
			if (!mapperMe)
			{
				((SimpleUIButton)BlockMapper.CurrentInstance.CopyButton).Click += new Click(OnCopy);
				((SimpleUIButton)BlockMapper.CurrentInstance.PasteButton).Click += new Click(OnPaste);
				mapperMe = true;
			}
		}
		else
		{
			mapperMe = false;
		}
		isFirstFrame = true;
	}

	private void FixedUpdate()
	{
		if (EnhancementEnabled && ((BasicInfo)BB).SimPhysics && !isFirstFrame)
		{
			SimulateFixedUpdate_EnhancementEnabled();
		}
	}

	private void LateUpdate()
	{
		if (EnhancementEnabled && ((BasicInfo)BB).SimPhysics && !isFirstFrame)
		{
			SimulateLateUpdate_EnhancementEnabled();
		}
	}

	[Obsolete]
	private void SaveConfiguration(PlayerMachineInfo pmi)
	{
		if (pmi == (PlayerMachineInfo)null)
		{
			return;
		}
		foreach (BlockInfo block in pmi.Blocks)
		{
			if (block.Guid == BB.Guid)
			{
				XDataHolder data = block.Data;
				try
				{
					BlockDataSaveEvent(data);
				}
				catch
				{
				}
				SaveConfiguration(data);
				break;
			}
		}
	}

	[Obsolete]
	private void LoadConfiguration()
	{
		if (SingleInstance<EnhancementBlockController>.Instance.PMI == (PlayerMachineInfo)null)
		{
			return;
		}
		foreach (BlockInfo block in SingleInstance<EnhancementBlockController>.Instance.PMI.Blocks)
		{
			if (block.Guid == BB.Guid)
			{
				XDataHolder data = block.Data;
				try
				{
					BlockDataLoadEvent(data);
				}
				catch
				{
				}
				LoadConfiguration(data);
				break;
			}
		}
	}

	[Obsolete]
	public virtual void SaveConfiguration(XDataHolder BlockData)
	{
	}

	[Obsolete]
	public virtual void LoadConfiguration(XDataHolder BlockData)
	{
	}

	public virtual void SafeAwake()
	{
	}

	public virtual void OnPlaced()
	{
	}

	public virtual void OnCopy()
	{
	}

	public virtual void OnPaste()
	{
	}

	public virtual void OnSimulateStartAlways()
	{
	}

	public virtual void OnSimulateStart_EnhancementEnabled()
	{
	}

	public virtual void SimulateUpdateHost_EnhancementEnabled()
	{
	}

	public virtual void SimulateUpdateClient_EnhancementEnabled()
	{
	}

	public virtual void SimulateUpdateAlways_EnhancementEnable()
	{
	}

	public virtual void SimulateUpdateAlways()
	{
	}

	public virtual void SimulateFixedUpdate_EnhancementEnabled()
	{
	}

	public virtual void SimulateLateUpdate_EnhancementEnabled()
	{
	}

	public virtual void BuildingUpdateAlways_EnhancementEnabled()
	{
	}

	public virtual void DisplayInMapper(bool enhance)
	{
	}

	public virtual void ChangedProperties(MapperType mapperType)
	{
	}

	public virtual void OnSimulateStartClient()
	{
	}

	public MKey AddKey(string displayName, string key, KeyCode defaultValue)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		MKey mapper = ((SaveableDataHolder)BB).AddKey(displayName, key, defaultValue);
		mapper.KeysChanged += (KeysChangeHandler)delegate
		{
			PropertiseChangedEvent((MapperType)(object)mapper);
		};
		return mapper;
	}

	public MSlider AddSlider(string displayName, string key, float defaultValue, float min, float max)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		MSlider mapper = ((SaveableDataHolder)BB).AddSlider(displayName, key, defaultValue, min, max, "", "x");
		mapper.ValueChanged += (ValueChangeHandler)delegate
		{
			if (Input.GetKeyUp((KeyCode)323))
			{
				PropertiseChangedEvent((MapperType)(object)mapper);
			}
		};
		return mapper;
	}

	public MLimits AddLimits(string displayName, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform fauxTransform)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		MLimits mapper = ((SaveableDataHolder)BB).AddLimits(displayName, key, defaultMin, defaultMax, highestAngle, fauxTransform, (ILimitsDisplay)(object)this);
		mapper.LimitsChanged += (LimitsChangeHandler)delegate
		{
			if (Input.GetKeyUp((KeyCode)323))
			{
				PropertiseChangedEvent((MapperType)(object)mapper);
			}
		};
		return mapper;
	}

	public MToggle AddToggle(string displayName, string key, bool defaultValue)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		MToggle mapper = ((SaveableDataHolder)BB).AddToggle(displayName, key, defaultValue);
		mapper.Toggled += (ToggleHandler)delegate
		{
			PropertiseChangedEvent((MapperType)(object)mapper);
		};
		return mapper;
	}

	public MMenu AddMenu(string key, int defaultIndex, List<string> items)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		MMenu mapper = ((SaveableDataHolder)BB).AddMenu(key, defaultIndex, items, false);
		mapper.ValueChanged += (ValueHandler)delegate
		{
			PropertiseChangedEvent((MapperType)(object)mapper);
		};
		return mapper;
	}

	public MColourSlider AddColourSlider(string displayName, string key, Color defaultValue, bool snapColors)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		MColourSlider mapper = ((SaveableDataHolder)BB).AddColourSlider(displayName, key, defaultValue, snapColors, false);
		mapper.ValueChanged += (ColourChangeHandler)delegate
		{
			if (Input.GetKeyUp((KeyCode)323))
			{
				PropertiseChangedEvent((MapperType)(object)mapper);
			}
		};
		return mapper;
	}

	public MValue AddValue(string displayName, string key, float defaultValue)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		MValue mapper = ((SaveableDataHolder)BB).AddValue(displayName, key, defaultValue);
		mapper.ValueChanged += (ValueChangeHandler)delegate
		{
			PropertiseChangedEvent((MapperType)(object)mapper);
		};
		return mapper;
	}

	public Transform GetLimitsDisplay()
	{
		return ((Component)((BasicInfo)BB.VisualController.Block).MeshRenderer).transform;
	}
}
