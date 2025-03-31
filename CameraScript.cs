using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlockEnhancementMod;

internal class CameraScript : EnhancementBlock
{
	private MToggle CameraLookAtToggle;

	public bool cameraLookAtToggled;

	private int selfIndex;

	public FixedCameraBlock fixedCamera;

	private Transform smoothLook;

	public FixedCameraController fixedCameraController;

	public MouseOrbit mouseOrbit;

	private Quaternion defaultLocalRotation;

	public float smooth;

	public float smoothLerp;

	private float newCamFOV;

	private float orgCamFOV;

	private float camFOVSmooth;

	private bool firstPerson;

	private MKey LockTargetKey;

	public Transform target;

	public List<KeyCode> lockKeys = new List<KeyCode> { (KeyCode)127 };

	private List<Collider> blockColliders = new List<Collider>();

	private HashSet<SimCluster> clustersInSafetyRange = new HashSet<SimCluster>();

	private MKey PauseTrackingKey;

	public bool pauseTracking;

	public List<KeyCode> pauseKeys = new List<KeyCode> { (KeyCode)120 };

	private MSlider NonCustomModeSmoothSlider;

	private MKey AutoLookAtKey;

	private MKey ZoomInKey;

	private MKey ZoomOutKey;

	private MSlider ZoomSpeedSlider;

	private MMenu ZoomControlModeMenu;

	private int zoomControlModeIndex;

	private float zoomSpeed = 2f;

	public List<string> zoomControlMode = new List<string>
	{
		SingleInstance<LanguageManager>.Instance.CurrentLanguage.MouseWheelZoomControl,
		SingleInstance<LanguageManager>.Instance.CurrentLanguage.KeyboardZoomControl
	};

	private bool firstPersonMode;

	private bool targetInitialCJOrHJ;

	public float firstPersonSmooth = 0.25f;

	private float timeOfDestruction;

	private readonly float targetSwitchDelay = 1.25f;

	public List<KeyCode> activeGuideKeys = new List<KeyCode> { (KeyCode)303 };

	private float searchAngle = 90f;

	private readonly float safetyRadiusAuto = 50f;

	private readonly float safetyRadiusManual = 15f;

	private bool autoSearch = true;

	private bool targetAquired;

	private bool searchStarted;

	private readonly float displayTime = 1f;

	private float switchTime = float.NegativeInfinity;

	private bool activateTimeRecorded;

	private readonly int bombValue = 64;

	private readonly int guidedRocketValue = 1024;

	private readonly int normalRocketValue = 512;

	private readonly int waterCannonValue = 16;

	private readonly int flyingBlockValue = 2;

	private readonly int flameThrowerValue = 8;

	private readonly int cogMotorValue = 2;

	private readonly GUIStyle camModeStyle;

	public override void SafeAwake()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		CameraLookAtToggle = ((SaveableDataHolder)base.BB).AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.TrackTarget, "TrackingCamera", cameraLookAtToggled);
		CameraLookAtToggle.Toggled += (ToggleHandler)delegate(bool value)
		{
			MKey lockTargetKey = LockTargetKey;
			MKey pauseTrackingKey = PauseTrackingKey;
			MSlider nonCustomModeSmoothSlider = NonCustomModeSmoothSlider;
			bool flag2 = (((MapperType)AutoLookAtKey).DisplayInMapper = value);
			bool flag4 = (((MapperType)nonCustomModeSmoothSlider).DisplayInMapper = flag2);
			bool flag6 = (((MapperType)pauseTrackingKey).DisplayInMapper = flag4);
			bool flag8 = (((MapperType)lockTargetKey).DisplayInMapper = flag6);
			cameraLookAtToggled = flag8;
			ChangedProperties((MapperType)(object)CameraLookAtToggle);
		};
		ZoomControlModeMenu = ((SaveableDataHolder)base.BB).AddMenu(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ZoomControlMode, zoomControlModeIndex, zoomControlMode, false);
		ZoomControlModeMenu.ValueChanged += (ValueHandler)delegate(int value)
		{
			zoomControlModeIndex = value;
			ChangedProperties((MapperType)(object)ZoomControlModeMenu);
		};
		NonCustomModeSmoothSlider = ((SaveableDataHolder)base.BB).AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.FirstPersonSmooth, "nonCustomSmooth", firstPersonSmooth, 0f, 1f, "", "x");
		NonCustomModeSmoothSlider.ValueChanged += (ValueChangeHandler)delegate(float value)
		{
			firstPersonSmooth = value;
			ChangedProperties((MapperType)(object)NonCustomModeSmoothSlider);
		};
		LockTargetKey = ((SaveableDataHolder)base.BB).AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.LockTarget, "LockTarget", (KeyCode)127);
		PauseTrackingKey = ((SaveableDataHolder)base.BB).AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.PauseTracking, "ResetView", (KeyCode)120);
		AutoLookAtKey = ((SaveableDataHolder)base.BB).AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ManualOverride, "ActiveSearchKey", (KeyCode)303);
		ZoomInKey = ((SaveableDataHolder)base.BB).AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ZoomIn, "ZoomInKey", (KeyCode)61);
		ZoomOutKey = ((SaveableDataHolder)base.BB).AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ZoomOut, "ZoomOutKey", (KeyCode)45);
		ZoomSpeedSlider = ((SaveableDataHolder)base.BB).AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ZoomSpeed, "ZoomSpeed", zoomSpeed, 0f, 20f, "", "x");
		ZoomSpeedSlider.ValueChanged += (ValueChangeHandler)delegate(float value)
		{
			zoomSpeed = value;
			ChangedProperties((MapperType)(object)ZoomSpeedSlider);
		};
		fixedCamera = ((Component)this).GetComponent<FixedCameraBlock>();
		smoothLook = fixedCamera.CompositeTracker3;
		defaultLocalRotation = smoothLook.localRotation;
		selfIndex = ((BlockBehaviour)fixedCamera).BuildIndex;
	}

	public override void DisplayInMapper(bool value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)fixedCamera.CamMode == 2)
		{
			firstPersonMode = true;
		}
		((MapperType)ZoomInKey).DisplayInMapper = value;
		((MapperType)ZoomOutKey).DisplayInMapper = value;
		((MapperType)ZoomSpeedSlider).DisplayInMapper = value;
		((MapperType)ZoomControlModeMenu).DisplayInMapper = value;
		((MapperType)CameraLookAtToggle).DisplayInMapper = value;
		((MapperType)NonCustomModeSmoothSlider).DisplayInMapper = value && cameraLookAtToggled && firstPersonMode;
		((MapperType)AutoLookAtKey).DisplayInMapper = value && cameraLookAtToggled;
		((MapperType)LockTargetKey).DisplayInMapper = value && cameraLookAtToggled;
		((MapperType)PauseTrackingKey).DisplayInMapper = value && cameraLookAtToggled;
	}

	public override void BuildingUpdateAlways_EnhancementEnabled()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Invalid comparison between Unknown and I4
		if ((int)fixedCamera.CamMode != 2 && firstPersonMode)
		{
			firstPersonMode = false;
			((MapperType)NonCustomModeSmoothSlider).DisplayInMapper = cameraLookAtToggled && firstPersonMode;
			MMenu zoomControlModeMenu = ZoomControlModeMenu;
			bool displayInMapper = (((MapperType)ZoomSpeedSlider).DisplayInMapper = firstPersonMode);
			((MapperType)zoomControlModeMenu).DisplayInMapper = displayInMapper;
		}
		if ((int)fixedCamera.CamMode == 2 && !firstPersonMode)
		{
			firstPersonMode = true;
			((MapperType)NonCustomModeSmoothSlider).DisplayInMapper = cameraLookAtToggled && firstPersonMode;
			MMenu zoomControlModeMenu2 = ZoomControlModeMenu;
			bool displayInMapper = (((MapperType)ZoomSpeedSlider).DisplayInMapper = firstPersonMode);
			((MapperType)zoomControlModeMenu2).DisplayInMapper = displayInMapper;
		}
		if (((MapperType)ZoomControlModeMenu).DisplayInMapper)
		{
			if (!((MapperType)ZoomInKey).DisplayInMapper && !((MapperType)ZoomOutKey).DisplayInMapper && zoomControlModeIndex == 1)
			{
				MKey zoomInKey = ZoomInKey;
				bool displayInMapper = (((MapperType)ZoomOutKey).DisplayInMapper = true);
				((MapperType)zoomInKey).DisplayInMapper = displayInMapper;
			}
			if (((MapperType)ZoomInKey).DisplayInMapper && ((MapperType)ZoomOutKey).DisplayInMapper && zoomControlModeIndex == 0)
			{
				MKey zoomInKey2 = ZoomInKey;
				bool displayInMapper = (((MapperType)ZoomOutKey).DisplayInMapper = false);
				((MapperType)zoomInKey2).DisplayInMapper = displayInMapper;
			}
		}
		if (!((MapperType)ZoomControlModeMenu).DisplayInMapper && ((MapperType)ZoomInKey).DisplayInMapper && ((MapperType)ZoomOutKey).DisplayInMapper)
		{
			MKey zoomInKey3 = ZoomInKey;
			bool displayInMapper = (((MapperType)ZoomOutKey).DisplayInMapper = ((MapperType)ZoomControlModeMenu).DisplayInMapper);
			((MapperType)zoomInKey3).DisplayInMapper = displayInMapper;
		}
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		firstPerson = (int)fixedCamera.CamMode == 2;
		fixedCameraController = Object.FindObjectOfType<FixedCameraController>();
		mouseOrbit = Object.FindObjectOfType<MouseOrbit>();
		foreach (FixedCameraBlock camera in fixedCameraController.cameras)
		{
			if (((BlockBehaviour)camera).BuildIndex == selfIndex)
			{
				if (firstPersonMode)
				{
					smooth = Mathf.Clamp01(firstPersonSmooth);
				}
				else
				{
					smooth = Mathf.Clamp01(camera.SmoothSlider.Value);
				}
				SetSmoothing();
			}
		}
		newCamFOV = (orgCamFOV = fixedCamera.fovSlider.Value);
		camFOVSmooth = Mathf.Exp(smooth) / Mathf.Exp(1f) / 2f;
		if (!cameraLookAtToggled)
		{
			return;
		}
		searchStarted = (targetInitialCJOrHJ = false);
		pauseTracking = (autoSearch = (targetAquired = true));
		float num = Mathf.Clamp(Mathf.Atan(Mathf.Tan(fixedCamera.fovSlider.Value * ((float)Math.PI / 180f) / 2f) * Camera.main.aspect) * 57.29578f, 0f, 90f);
		searchAngle = Mathf.Clamp(searchAngle, 0f, num);
		target = null;
		if (!StatMaster.isMP)
		{
			clustersInSafetyRange.Clear();
			SimCluster[] simClusters = Machine.Active().simClusters;
			foreach (SimCluster val in simClusters)
			{
				Vector3 val2 = ((Component)val.Base).transform.position - ((Component)fixedCamera).transform.position;
				if (((Vector3)(ref val2)).magnitude < safetyRadiusAuto)
				{
					clustersInSafetyRange.Add(val);
				}
			}
		}
		((MonoBehaviour)this).StopAllCoroutines();
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)fixedCameraController?.activeCamera?.CompositeTracker3 == (Object)(object)smoothLook))
		{
			return;
		}
		if (firstPerson)
		{
			Camera cam = mouseOrbit.cam;
			if (zoomControlModeIndex == 0)
			{
				if (Input.GetAxis("Mouse ScrollWheel") != 0f)
				{
					newCamFOV = Mathf.Clamp(cam.fieldOfView - Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")) * zoomSpeed, 1f, orgCamFOV);
				}
			}
			else
			{
				if (ZoomInKey.IsHeld || ZoomInKey.EmulationHeld(false))
				{
					newCamFOV = Mathf.Clamp(cam.fieldOfView - zoomSpeed, 1f, orgCamFOV);
				}
				if (ZoomOutKey.IsHeld || ZoomOutKey.EmulationHeld(false))
				{
					newCamFOV = Mathf.Clamp(cam.fieldOfView + zoomSpeed, 1f, orgCamFOV);
				}
			}
			if (cam.fieldOfView != newCamFOV)
			{
				cam.fieldOfView = Mathf.SmoothStep(cam.fieldOfView, newCamFOV, camFOVSmooth);
			}
		}
		if (cameraLookAtToggled)
		{
			if (!activateTimeRecorded)
			{
				switchTime = Time.time;
				activateTimeRecorded = true;
			}
			if (AutoLookAtKey.IsReleased || AutoLookAtKey.EmulationReleased())
			{
				autoSearch = !autoSearch;
				switchTime = Time.time;
			}
			if (PauseTrackingKey.IsReleased || PauseTrackingKey.EmulationReleased())
			{
				pauseTracking = !pauseTracking;
			}
			if (LockTargetKey.IsReleased || LockTargetKey.EmulationReleased())
			{
				target = null;
				if (autoSearch)
				{
					targetAquired = (searchStarted = false);
					CameraRadarSearch();
				}
				else
				{
					if (StatMaster.isClient)
					{
						blockColliders.Clear();
						foreach (PlayerData player in Playerlist.Players)
						{
							if (!player.isSpectator && ((Machine)player.machine).isSimulating)
							{
								blockColliders.AddRange(((Component)((Machine)player.machine).SimulationMachine).GetComponentsInChildren<Collider>(true));
							}
						}
						foreach (Collider blockCollider in blockColliders)
						{
							blockCollider.enabled = true;
						}
					}
					Ray val = Camera.main.ScreenPointToRay(Input.mousePosition);
					float num = 1.25f;
					RaycastHit[] array = Physics.SphereCastAll(val, num, float.PositiveInfinity);
					RaycastHit val2 = default(RaycastHit);
					Physics.Raycast(val, ref val2);
					Vector3 val3;
					if (array.Length != 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (Object.op_Implicit((Object)(object)((Component)((RaycastHit)(ref array[i])).transform).gameObject.GetComponent<BlockBehaviour>()))
							{
								val3 = ((RaycastHit)(ref array[i])).transform.position - ((Component)fixedCamera).transform.position;
								if (((Vector3)(ref val3)).magnitude >= safetyRadiusManual)
								{
									target = ((RaycastHit)(ref array[i])).transform;
									pauseTracking = false;
									break;
								}
							}
						}
						if ((Object)(object)target == (Object)null)
						{
							for (int j = 0; j < array.Length; j++)
							{
								if (Object.op_Implicit((Object)(object)((Component)((RaycastHit)(ref array[j])).transform).gameObject.GetComponent<LevelEntity>()))
								{
									val3 = ((RaycastHit)(ref array[j])).transform.position - ((Component)fixedCamera).transform.position;
									if (((Vector3)(ref val3)).magnitude >= safetyRadiusManual)
									{
										target = ((RaycastHit)(ref array[j])).transform;
										break;
									}
								}
							}
						}
					}
					if ((Object)(object)target == (Object)null && (Object)(object)((RaycastHit)(ref val2)).transform != (Object)null)
					{
						val3 = ((RaycastHit)(ref val2)).transform.position - ((Component)fixedCamera).transform.position;
						if (((Vector3)(ref val3)).magnitude >= safetyRadiusManual)
						{
							target = ((RaycastHit)(ref val2)).transform;
							pauseTracking = false;
						}
					}
					if (StatMaster.isClient)
					{
						foreach (Collider blockCollider2 in blockColliders)
						{
							blockCollider2.enabled = false;
						}
					}
				}
			}
		}
		if ((Object)(object)fixedCameraController.activeCamera != (Object)(object)fixedCamera && activateTimeRecorded)
		{
			activateTimeRecorded = false;
		}
	}

	public override void SimulateFixedUpdate_EnhancementEnabled()
	{
		if (!cameraLookAtToggled || !((Object)(object)fixedCameraController?.activeCamera?.CompositeTracker3 == (Object)(object)smoothLook))
		{
			return;
		}
		if (autoSearch && !targetAquired)
		{
			CameraRadarSearch();
		}
		if (!((Object)(object)target != (Object)null))
		{
			return;
		}
		Debug.Log((object)"??");
		try
		{
			if (targetInitialCJOrHJ && (Object)(object)((Component)target).gameObject.GetComponent<ConfigurableJoint>() == (Object)null && (Object)(object)((Component)target).gameObject.GetComponent<HingeJoint>() == (Object)null)
			{
				timeOfDestruction = Time.time;
				targetInitialCJOrHJ = false;
				targetAquired = false;
				target = null;
				return;
			}
		}
		catch
		{
		}
		try
		{
			if (((Component)target).gameObject.GetComponent<TimedRocket>().hasExploded)
			{
				timeOfDestruction = Time.time;
				targetAquired = false;
				target = null;
				return;
			}
		}
		catch
		{
		}
		try
		{
			if (((Component)target).gameObject.GetComponent<ExplodeOnCollideBlock>().hasExploded)
			{
				timeOfDestruction = Time.time;
				targetAquired = false;
				target = null;
				return;
			}
		}
		catch
		{
		}
		try
		{
			if (((Component)target).gameObject.GetComponent<ExplodeOnCollide>().hasExploded)
			{
				timeOfDestruction = Time.time;
				targetAquired = false;
				target = null;
				return;
			}
		}
		catch
		{
		}
		try
		{
			if (((Component)target).gameObject.GetComponent<ControllableBomb>().hasExploded)
			{
				timeOfDestruction = Time.time;
				targetAquired = false;
				target = null;
			}
		}
		catch
		{
		}
	}

	public override void SimulateLateUpdate_EnhancementEnabled()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		if (!cameraLookAtToggled || !((Object)(object)fixedCameraController?.activeCamera?.CompositeTracker3 == (Object)(object)smoothLook))
		{
			return;
		}
		if (pauseTracking)
		{
			smoothLook.localRotation = Quaternion.Slerp(smoothLook.localRotation, defaultLocalRotation, smoothLerp * Time.deltaTime);
		}
		else if (Time.time - timeOfDestruction >= targetSwitchDelay)
		{
			if ((Object)(object)target == (Object)null)
			{
				smoothLook.localRotation = Quaternion.Slerp(smoothLook.localRotation, defaultLocalRotation, smoothLerp * Time.deltaTime);
				return;
			}
			Quaternion val = ((!firstPersonMode) ? Quaternion.LookRotation(target.position - smoothLook.position) : Quaternion.LookRotation(target.position - smoothLook.position, fixedCamera.CompositeTracker2.up));
			smoothLook.rotation = Quaternion.Slerp(smoothLook.rotation, val, smoothLerp * Time.deltaTime);
		}
	}

	private void SetSmoothing()
	{
		float num = 1f - smooth;
		smoothLerp = 16.126f * num * num - 1.286f * num + 0.287f;
	}

	private void CameraRadarSearch()
	{
		if (!searchStarted && autoSearch)
		{
			searchStarted = true;
			((MonoBehaviour)this).StopCoroutine(SearchForTarget());
			((MonoBehaviour)this).StartCoroutine(SearchForTarget());
		}
	}

	private void GetMostValuableCluster(HashSet<SimCluster> simClusterForSearch, out Transform targetTransform, out float targetClusterValue)
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		simClusterForSearch.RemoveWhere((SimCluster cluster) => cluster == null);
		int[] array = new int[simClusterForSearch.Count];
		SimCluster[] array2 = (SimCluster[])(object)new SimCluster[simClusterForSearch.Count];
		List<SimCluster> list = new List<SimCluster>();
		int num = 0;
		foreach (SimCluster item in simClusterForSearch)
		{
			int clusterValue = item.Blocks.Length + 1;
			clusterValue = CalculateClusterValue(item.Base, clusterValue);
			BlockBehaviour[] blocks = item.Blocks;
			foreach (BlockBehaviour block in blocks)
			{
				clusterValue = CalculateClusterValue(block, clusterValue);
			}
			array[num] = clusterValue;
			array2[num] = item;
			num++;
		}
		int num2 = array.Max();
		for (num = 0; num < array.Length; num++)
		{
			if (array[num] == num2)
			{
				list.Add(array2[num]);
			}
		}
		int index = 0;
		float num3 = 180f;
		for (num = 0; num < list.Count; num++)
		{
			Vector3 val = ((Component)list[num].Base).gameObject.transform.position - smoothLook.position;
			float num4 = Vector3.Angle(((Vector3)(ref val)).normalized, smoothLook.forward);
			if (num4 < num3)
			{
				index = num;
				num3 = num4;
			}
		}
		targetTransform = ((Component)list[index].Base).gameObject.transform;
		targetClusterValue = num2;
	}

	private bool CheckInRange(BlockBehaviour target)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((Component)target).gameObject.transform.position - smoothLook.position;
		float num = Vector3.Angle(((Vector3)(ref val)).normalized, smoothLook.forward);
		if (Vector3.Dot(val, smoothLook.forward) > 0f)
		{
			return num < searchAngle;
		}
		return false;
	}

	private IEnumerator SearchForTarget()
	{
		Dictionary<BlockBehaviour, int> rocketTargetDict = SingleInstance<RocketsController>.Instance.rocketTargetDict;
		Transform rocketTarget = null;
		Transform clusterTarget = null;
		float rocketValue = 0f;
		float clusterValue = 0f;
		if (rocketTargetDict != null && rocketTargetDict.Count > 0)
		{
			float num = float.PositiveInfinity;
			foreach (KeyValuePair<BlockBehaviour, int> item in rocketTargetDict)
			{
				BlockBehaviour key = item.Key;
				if (!((Object)(object)key != (Object)null))
				{
					continue;
				}
				bool flag;
				Vector3 val;
				if (StatMaster.isMP)
				{
					flag = ((BasicInfo)key).ParentMachine.PlayerID != ((BasicInfo)fixedCamera).ParentMachine.PlayerID && ((int)((BlockBehaviour)fixedCamera).Team == 0 || ((BlockBehaviour)fixedCamera).Team != key.Team);
				}
				else if (key.ClusterIndex == -1)
				{
					val = ((Component)key).transform.position - ((Component)fixedCamera).transform.position;
					flag = ((Vector3)(ref val)).magnitude > safetyRadiusAuto;
				}
				else
				{
					int num2 = 0;
					foreach (SimCluster item2 in clustersInSafetyRange)
					{
						if (item2.Base.ClusterIndex == key.ClusterIndex)
						{
							num2++;
						}
					}
					flag = num2 <= 0;
				}
				if (CheckInRange(key) && flag)
				{
					val = ((Component)key).transform.position - ((Component)fixedCamera).transform.position;
					float magnitude = ((Vector3)(ref val)).magnitude;
					if (magnitude <= num)
					{
						rocketTarget = ((Component)key).transform;
						num = magnitude;
						rocketValue = guidedRocketValue;
					}
				}
			}
		}
		yield return (object)new WaitForEndOfFrame();
		HashSet<SimCluster> simClusters = new HashSet<SimCluster>();
		if (StatMaster.isMP)
		{
			foreach (PlayerData player in Playerlist.Players)
			{
				if (!player.isSpectator && ((Machine)player.machine).isSimulating && !((Machine)player.machine).LocalSim && ((Machine)player.machine).PlayerID != ((BasicInfo)fixedCamera).ParentMachine.PlayerID && ((int)((BlockBehaviour)fixedCamera).Team == 0 || ((BlockBehaviour)fixedCamera).Team != player.team))
				{
					simClusters.UnionWith(((Machine)player.machine).simClusters);
				}
			}
		}
		else
		{
			simClusters.UnionWith(Machine.Active().simClusters);
			clustersInSafetyRange.RemoveWhere((SimCluster cluster) => cluster == null);
			if (clustersInSafetyRange.Count > 0)
			{
				simClusters.ExceptWith(clustersInSafetyRange);
			}
		}
		while (!targetAquired && simClusters.Count > 0)
		{
			try
			{
				simClusters.RemoveWhere((SimCluster cluster) => cluster == null);
				HashSet<SimCluster> hashSet = new HashSet<SimCluster>(simClusters);
				HashSet<SimCluster> hashSet2 = new HashSet<SimCluster>();
				foreach (SimCluster item3 in simClusters)
				{
					bool flag2 = !CheckInRange(item3.Base) || ShouldSkipCluster(item3.Base);
					if (!flag2)
					{
						BlockBehaviour[] blocks = item3.Blocks;
						foreach (BlockBehaviour block in blocks)
						{
							flag2 = ShouldSkipCluster(block);
							if (flag2)
							{
								break;
							}
						}
					}
					if (flag2)
					{
						hashSet2.Add(item3);
					}
				}
				hashSet.ExceptWith(hashSet2);
				if (hashSet.Count > 0)
				{
					GetMostValuableCluster(hashSet, out clusterTarget, out clusterValue);
				}
			}
			catch
			{
			}
			if ((Object)(object)rocketTarget != (Object)null || (Object)(object)clusterTarget != (Object)null)
			{
				target = ((rocketValue >= clusterValue) ? rocketTarget : clusterTarget);
				targetAquired = true;
				pauseTracking = false;
				searchStarted = false;
				targetInitialCJOrHJ = (Object)(object)((Component)target).gameObject.GetComponent<ConfigurableJoint>() != (Object)null || (Object)(object)((Component)target).gameObject.GetComponent<HingeJoint>() != (Object)null;
			}
			yield return null;
		}
	}

	private int CalculateClusterValue(BlockBehaviour block, int clusterValue)
	{
		GameObject gameObject = ((Component)block).gameObject;
		if (block.BlockID == 23 && !gameObject.GetComponent<ExplodeOnCollideBlock>().hasExploded)
		{
			clusterValue *= bombValue;
		}
		if (block.BlockID == 59 && gameObject.GetComponent<TimedRocket>().hasFired)
		{
			clusterValue = ((!((Object)(object)gameObject.GetComponent<RocketScript>().radar != (Object)null)) ? (clusterValue * normalRocketValue) : (clusterValue * guidedRocketValue));
		}
		if (block.BlockID == 56 && gameObject.GetComponent<WaterCannonController>().isActive)
		{
			clusterValue *= waterCannonValue;
		}
		if (block.BlockID == 14 && gameObject.GetComponent<FlyingController>().canFly)
		{
			clusterValue *= flyingBlockValue;
		}
		if (block.BlockID == 21 && gameObject.GetComponent<FlamethrowerController>().isFlaming)
		{
			clusterValue *= flameThrowerValue;
		}
		if (Object.op_Implicit((Object)(object)gameObject.GetComponent<CogMotorControllerHinge>()) && gameObject.GetComponent<CogMotorControllerHinge>().Velocity != 0f)
		{
			clusterValue *= cogMotorValue;
		}
		return clusterValue;
	}

	private bool ShouldSkipCluster(BlockBehaviour block)
	{
		try
		{
			if (block.BlockID == 59)
			{
				if (((Component)block).gameObject.GetComponent<TimedRocket>().hasExploded)
				{
					return true;
				}
			}
			else
			{
				if (block.fireTag.burning)
				{
					return true;
				}
				try
				{
					if (((Component)block).gameObject.GetComponent<ExplodeOnCollideBlock>().hasExploded)
					{
						return true;
					}
				}
				catch
				{
				}
				try
				{
					if (((Component)block).gameObject.GetComponent<ControllableBomb>().hasExploded)
					{
						return true;
					}
				}
				catch
				{
				}
			}
		}
		catch
		{
		}
		return false;
	}

	private void OnGUI()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)fixedCameraController?.activeCamera?.CompositeTracker3 == (Object)(object)smoothLook && (Time.time - switchTime) / Time.timeScale <= displayTime)
		{
			GUI.TextArea(new Rect(1f, 1f, 20f, 150f), "CAM TRACKING: " + (autoSearch ? "AUTO" : "MANUAL"), camModeStyle);
		}
	}

	public CameraScript()
	{
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		GUIStyle val = new GUIStyle
		{
			fontStyle = (FontStyle)1,
			fontSize = 16
		};
		val.normal.textColor = Color.white;
		val.alignment = (TextAnchor)0;
		camModeStyle = val;
		base._002Ector();
	}
}
