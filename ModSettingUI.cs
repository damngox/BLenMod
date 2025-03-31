using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlockEnhancementMod;

internal class ModSettingUI : SafeUIBehaviour
{
	public bool showGUI = BlockEnhancementMod.ModSetting.ShowUI;

	public bool Friction = BlockEnhancementMod.ModSetting.Friction;

	public bool BuildSurface_Collision_Mass = BlockEnhancementMod.ModSetting.BuildSurface_Collision_Mass;

	public Action<bool> OnFrictionToggle;

	public override bool ShouldShowGUI { get; set; } = true;

	public override void SafeAwake()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		windowRect = new Rect(15f, 100f, 180f, 70f);
		base.windowName = SingleInstance<LanguageManager>.Instance.CurrentLanguage.ModSettings + "  Ctrl+F9";
		LanguageManager instance = SingleInstance<LanguageManager>.Instance;
		instance.OnLanguageChanged = (Action<string>)Delegate.Combine(instance.OnLanguageChanged, (Action<string>)delegate
		{
			base.windowName = SingleInstance<LanguageManager>.Instance.CurrentLanguage.ModSettings + "  Ctrl+F9";
		});
	}

	public override void OnGUI()
	{
		base.OnGUI();
	}

	private void Update()
	{
		if (Input.GetKey((KeyCode)306) && Input.GetKeyDown((KeyCode)290))
		{
			BlockEnhancementMod.ModSetting.ShowUI = (showGUI = !showGUI);
		}
		ShouldShowGUI = showGUI && !StatMaster.levelSimulating && IsBuilding() && !StatMaster.inMenu;
	}

	private bool IsBuilding()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string> { "INITIALISER", "TITLE SCREEN", "LevelSelect", "LevelSelect1", "LevelSelect2", "LevelSelect3" };
		Scene activeScene = SceneManager.GetActiveScene();
		if (((Scene)(ref activeScene)).isLoaded)
		{
			if (!list.Exists(delegate(string match)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				Scene activeScene2 = SceneManager.GetActiveScene();
				return match == ((Scene)(ref activeScene2)).name;
			}))
			{
				return true;
			}
			return false;
		}
		return false;
	}

	protected override void WindowContent(int windowID)
	{
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		GUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		GUILayout.Space(10f);
		GUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (!StatMaster.isClient)
		{
			if (EnhancementBlock.EnhanceMore != AddToggle(EnhancementBlock.EnhanceMore, new GUIContent(SingleInstance<LanguageManager>.Instance.CurrentLanguage.AdditionalFunction)))
			{
				BlockEnhancementMod.ModSetting.EnhanceMore = (EnhancementBlock.EnhanceMore = !EnhancementBlock.EnhanceMore);
			}
			if (Friction != AddToggle(Friction, new GUIContent(SingleInstance<LanguageManager>.Instance.CurrentLanguage.UnifiedFriction)))
			{
				BlockEnhancementMod.ModSetting.Friction = (Friction = !Friction);
				FrictionToggle(Friction);
				OnFrictionToggle?.Invoke(Friction);
			}
		}
		if (BuildSurface_Collision_Mass != AddToggle(BuildSurface_Collision_Mass, new GUIContent(SingleInstance<LanguageManager>.Instance.CurrentLanguage.BuildSurface)))
		{
			BlockEnhancementMod.ModSetting.BuildSurface_Collision_Mass = (BuildSurface_Collision_Mass = !BuildSurface_Collision_Mass);
			BuildSurface.ShowCollisionToggle = (BuildSurface.ShowMassSlider = BuildSurface_Collision_Mass);
		}
		if (RocketsController.DisplayWarning != AddToggle(RocketsController.DisplayWarning, new GUIContent(SingleInstance<LanguageManager>.Instance.CurrentLanguage.DisplayWarning)))
		{
			BlockEnhancementMod.ModSetting.DisplayWarning = (RocketsController.DisplayWarning = !RocketsController.DisplayWarning);
		}
		/*Error near IL_0161: Invalid metadata token*/;
		static void FrictionToggle(bool value)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			PhysicMaterialCombine val = (PhysicMaterialCombine)((!value) ? 3 : 0);
			if ((Object)(object)GameObject.Find("Terrain Terraced") != (Object)null)
			{
				MeshCollider[] componentsInChildren = GameObject.Find("Terrain Terraced").GetComponentsInChildren<MeshCollider>();
				int num = 0;
				if (num < componentsInChildren.Length)
				{
					MeshCollider obj = componentsInChildren[num];
					((Collider)obj).sharedMaterial.frictionCombine = val;
					((Collider)obj).sharedMaterial.bounceCombine = val;
				}
			}
		}
	}

	private bool AddToggle(bool value, string text)
	{
		value = GUILayout.Toggle(value, text, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		return value;
	}

	private bool AddToggle(bool value, GUIContent content)
	{
		return GUILayout.Toggle(value, content, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}
