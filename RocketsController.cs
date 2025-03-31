using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace BlockEnhancementMod;

internal class RocketsController : SingleInstance<RocketsController>
{
	private bool iAmLockedByRocket;

	private bool isFirstFrame = true;

	public static bool DisplayWarning = BlockEnhancementMod.ModSetting.DisplayWarning;

	private FixedCameraController cameraController;

	public Dictionary<BlockBehaviour, int> rocketTargetDict;

	public Dictionary<int, Dictionary<KeyCode, HashSet<TimedRocket>>> playerGroupedRockets;

	public Dictionary<KeyCode, HashSet<TimedRocket>> groupedRockets;

	public bool launchStarted;

	private static readonly float transparancy = 0.5f;

	private static readonly float screenOffset = 128f;

	private static readonly float warningHeight = 60f;

	private static readonly float warningWidth = 180f;

	private static readonly float borderThickness = 10f;

	private static readonly Color warningBorderColor = new Color(1f, 0f, 0f, transparancy);

	private static readonly Rect warningRect = new Rect((float)Screen.width - screenOffset - warningWidth, (float)Screen.height - screenOffset - warningHeight, warningWidth, warningHeight);

	private static Texture2D redTexture;

	public static Texture2D redSquareAim = new Texture2D(16, 16);

	public static Texture2D redCircleAim = new Texture2D(64, 64);

	private Rect counterRect = new Rect((float)Screen.width - screenOffset - warningWidth, (float)Screen.height - 0.5f * screenOffset - warningHeight, warningWidth, warningHeight);

	public static bool DisplayRocketCount = BlockEnhancementMod.ModSetting.DisplayRocketCount;

	private readonly GUIStyle missileWarningStyle;

	private readonly GUIStyle groupedRocketsCounterStyle;

	public override string Name { get; } = "Rockets Controller";

	private static Texture2D RedTexture
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)redTexture == (Object)null)
			{
				redTexture = new Texture2D(1, 1);
				redTexture.SetPixel(0, 0, warningBorderColor);
				redTexture.Apply();
			}
			return redTexture;
		}
	}

	public RocketsController()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		GUIStyle val = new GUIStyle
		{
			fontStyle = (FontStyle)1,
			fontSize = 16
		};
		val.normal.textColor = new Color(1f, 0f, 0f, transparancy);
		val.alignment = (TextAnchor)4;
		missileWarningStyle = val;
		GUIStyle val2 = new GUIStyle
		{
			fontStyle = (FontStyle)1,
			fontSize = 16
		};
		val2.normal.textColor = new Color(0f, 1f, 0f, transparancy);
		val2.alignment = (TextAnchor)4;
		groupedRocketsCounterStyle = val2;
		base._002Ector();
		rocketTargetDict = new Dictionary<BlockBehaviour, int>();
		playerGroupedRockets = new Dictionary<int, Dictionary<KeyCode, HashSet<TimedRocket>>>();
		InitRedSquareAndLayer();
		static void InitRedSquareAndLayer()
		{
			redSquareAim.LoadImage(ModIO.ReadAllBytes("Resources/Square-Red.png", false));
			redCircleAim.LoadImage(ModIO.ReadAllBytes("Resources/Circle-Red.png", false));
			SetRadarIgnoreCollosionLayer();
			static void SetRadarIgnoreCollosionLayer()
			{
				/*Error: Invalid metadata token*/;
			}
		}
	}

	private void FixedUpdate()
	{
		if (!StatMaster.levelSimulating)
		{
			if (playerGroupedRockets.Count > 0)
			{
				playerGroupedRockets.Clear();
			}
			/*Error near IL_0020: Invalid metadata token*/;
		}
		if (PlayerMachine.GetLocal() != (PlayerMachine)null)
		{
			if (PlayerMachine.GetLocal().InternalObject.isSimulating)
			{
				if (isFirstFrame)
				{
					isFirstFrame = (launchStarted = false);
					cameraController = Object.FindObjectOfType<FixedCameraController>();
					rocketTargetDict.Clear();
				}
			}
			else if (!isFirstFrame)
			{
				if (playerGroupedRockets.ContainsKey(PlayerMachine.GetLocal().InternalObject.PlayerID))
				{
					playerGroupedRockets.Remove(PlayerMachine.GetLocal().InternalObject.PlayerID);
				}
				rocketTargetDict.Clear();
				isFirstFrame = true;
			}
		}
		if (isFirstFrame || !(PlayerMachine.GetLocal() != (PlayerMachine)null) || rocketTargetDict == null || isFirstFrame)
		{
			return;
		}
		if (rocketTargetDict.Count == 0)
		{
			iAmLockedByRocket = false;
			return;
		}
		foreach (KeyValuePair<BlockBehaviour, int> item in rocketTargetDict)
		{
			if (PlayerMachine.GetLocal() != (PlayerMachine)null)
			{
				if (item.Value == (StatMaster.isMP ? PlayerMachine.GetLocal().Player.NetworkId : 0))
				{
					iAmLockedByRocket = true;
					break;
				}
				iAmLockedByRocket = false;
			}
			else
			{
				iAmLockedByRocket = false;
			}
		}
	}

	private void DrawBorder()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		Rect val = warningRect;
		float xMin = ((Rect)(ref val)).xMin;
		val = warningRect;
		float yMin = ((Rect)(ref val)).yMin;
		val = warningRect;
		GUI.DrawTexture(new Rect(xMin, yMin, ((Rect)(ref val)).width, borderThickness), (Texture)(object)RedTexture);
		val = warningRect;
		float xMin2 = ((Rect)(ref val)).xMin;
		val = warningRect;
		float num = ((Rect)(ref val)).yMin + borderThickness;
		float num2 = borderThickness;
		val = warningRect;
		GUI.DrawTexture(new Rect(xMin2, num, num2, ((Rect)(ref val)).height - 2f * borderThickness), (Texture)(object)RedTexture);
		val = warningRect;
		float num3 = ((Rect)(ref val)).xMax - borderThickness;
		val = warningRect;
		float num4 = ((Rect)(ref val)).yMin + borderThickness;
		float num5 = borderThickness;
		val = warningRect;
		GUI.DrawTexture(new Rect(num3, num4, num5, ((Rect)(ref val)).height - 2f * borderThickness), (Texture)(object)RedTexture);
		val = warningRect;
		float xMin3 = ((Rect)(ref val)).xMin;
		val = warningRect;
		float num6 = ((Rect)(ref val)).yMax - borderThickness;
		val = warningRect;
		GUI.DrawTexture(new Rect(xMin3, num6, ((Rect)(ref val)).width, borderThickness), (Texture)(object)RedTexture);
	}

	private void OnGUI()
	{
		DisplayMissleAlert();
		DisplayRemainingRocketCount();
	}

	private void DisplayMissleAlert()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Invalid comparison between Unknown and I4
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (DisplayWarning && iAmLockedByRocket && !((Object)(object)cameraController == (Object)null) && !((Object)(object)cameraController.activeCamera == (Object)null) && (int)cameraController.activeCamera.CamMode == 2)
		{
			DrawBorder();
			GUI.Box(warningRect, "Missile Alert", missileWarningStyle);
		}
	}

	private void DisplayRemainingRocketCount()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		if (!DisplayRocketCount || (Object)(object)cameraController == (Object)null || (Object)(object)cameraController.activeCamera == (Object)null || (int)cameraController.activeCamera.CamMode != 2 || !playerGroupedRockets.TryGetValue(StatMaster.isMP ? PlayerMachine.GetLocal().Player.NetworkId : 0, out groupedRockets))
		{
			return;
		}
		string text = "";
		foreach (KeyValuePair<KeyCode, HashSet<TimedRocket>> groupedRocket in groupedRockets)
		{
			text = text + KeyCodeConverter.GetKey(groupedRocket.Key).ToString() + ": " + groupedRocket.Value.Count + Environment.NewLine;
		}
		GUI.Box(counterRect, SingleInstance<LanguageManager>.Instance.CurrentLanguage.RemainingRockets + Environment.NewLine + text, groupedRocketsCounterStyle);
	}

	public void UpdateRocketFiredStatus(TimedRocket rocket)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (playerGroupedRockets.TryGetValue(StatMaster.isMP ? PlayerMachine.GetLocal().Player.NetworkId : 0, out var value))
		{
			RocketScript component = ((Component)rocket).GetComponent<RocketScript>();
			if ((Object)(object)component != (Object)null && value.TryGetValue(component.GroupFireKey.GetKey(0), out var value2))
			{
				value2.Remove(rocket);
			}
		}
	}

	public void UpdateRocketTarget(BlockBehaviour rocket, int targetMachineID)
	{
		if (rocketTargetDict.ContainsKey(rocket))
		{
			rocketTargetDict[rocket] = targetMachineID;
		}
		else
		{
			rocketTargetDict.Add(rocket, targetMachineID);
		}
	}

	public void RemoveRocketTarget(BlockBehaviour rocket)
	{
		if (rocketTargetDict.ContainsKey(rocket))
		{
			rocketTargetDict.Remove(rocket);
		}
	}

	public IEnumerator LaunchRocketFromGroup(int id, KeyCode key)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (!playerGroupedRockets.TryGetValue(id, out var timedRocketDict))
		{
			launchStarted = false;
			yield return null;
		}
		if (!timedRocketDict.TryGetValue(key, out var timedRockets))
		{
			launchStarted = false;
			yield return null;
		}
		launchStarted = true;
		float delay = 0.25f;
		if (timedRockets.Count > 0)
		{
			TimedRocket rocket = timedRockets.First();
			timedRockets.Remove(rocket);
			if ((Object)(object)rocket == (Object)null)
			{
				yield return null;
			}
			RocketScript rocketScript = ((Component)rocket).GetComponent<RocketScript>();
			if ((Object)(object)rocketScript == (Object)null)
			{
				yield return null;
			}
			if (rocketScript.AutoEjectToggle.IsActive)
			{
				((MonoBehaviour)this).StartCoroutine(ReleaseGrabbers(rocket));
				((MonoBehaviour)this).StartCoroutine(ReleaseEjectors(rocket));
			}
			delay = Mathf.Clamp(rocketScript.GroupFireRateSlider.Value, 0.1f, 1f);
			rocket.LaunchMessage();
		}
		((MonoBehaviour)this).StartCoroutine(ResetLaunchState(delay));
		yield return null;
	}

	private IEnumerator ResetLaunchState(float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		launchStarted = false;
	}

	private IEnumerator ReleaseGrabbers(TimedRocket rocket)
	{
		if (((BlockBehaviour)rocket).grabbers.Count > 0)
		{
			yield return null;
		}
		foreach (JoinOnTriggerBlock item in new List<JoinOnTriggerBlock>(((BlockBehaviour)rocket).grabbers))
		{
			if (item != null)
			{
				item.OnKeyPressed();
			}
		}
		yield return null;
	}

	private IEnumerator ReleaseEjectors(TimedRocket rocket)
	{
		List<Joint> list = new List<Joint>(((BlockBehaviour)rocket).iJointTo);
		list.AddRange(((BlockBehaviour)rocket).jointsToMe);
		foreach (Joint item in list)
		{
			if ((Object)(object)item == (Object)null)
			{
				continue;
			}
			ExplosiveBolt component = ((Component)item).gameObject.GetComponent<ExplosiveBolt>();
			if (!((Object)(object)component == (Object)null))
			{
				if (component != null)
				{
					component.Explode();
				}
				break;
			}
		}
		yield return null;
	}
}
