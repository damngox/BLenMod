using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Common;
using UnityEngine;

namespace BlockEnhancementMod;

internal class RocketScript : EnhancementBlock
{
	public struct TrailSmokePropertise
	{
		public float Lifetime;

		public float Size;

		public Color StartColor;

		public Color EndColor;

		public float StartColorTime;

		public float EndColorTime;

		public float StartAlpha;

		public float EndAlpha;

		public float StartAlphaTime;

		public float EndAlphaTime;

		public float EmissionConstant { get; set; }

		public override string ToString()
		{
			return StartAlphaTime + "-" + EndAlphaTime;
		}
	}

	private MToggle GuidedRocketToggle;

	private MKey LockTargetKey;

	private MMenu SettingMenu;

	public MKey GroupFireKey;

	public MSlider GroupFireRateSlider;

	public MToggle AutoEjectToggle;

	public TimedRocket rocket;

	public Rigidbody rocketRigidbody;

	private ParticleSystem smokeTrail;

	public static TrailSmokePropertise trailSmokePropertise;

	private MToggle NoSmokeToggle;

	private float launchTime;

	private bool launchTimeRecorded;

	public bool removedFromGroup;

	private MSlider TorqueSlider;

	private MToggle StabilityToggle;

	private MToggle ShowRadarToggle;

	private MToggle ShowPredictionToggle;

	private MSlider ProjectileSpeedSlider;

	private MSlider DragSlider;

	public bool rocketExploMsgSent;

	public bool rocketInBuildSent;

	private MSlider ActiveGuideRocketSearchAngleSlider;

	private MMenu RadarTypeMenu;

	private MKey ManualOverrideKey;

	public MKey SPTeamKey;

	private readonly float maxSearchAngleNormal = 90f;

	private readonly float maxSearchAngleNo8 = 175f;

	public GameObject radarObject;

	public RadarScript radar;

	public GameObject guideObject;

	public GuideController guideController;

	private MToggle ImpactFuzeToggle;

	private MToggle ProximityFuzeToggle;

	private MSlider ProximityFuzeRangeSlider;

	public float triggerForceImpactFuzeOn = 50f;

	public float triggerForceImpactFuzeOff = 1200f;

	private MSlider GuideDelaySlider;

	private MToggle HighExploToggle;

	private bool bombHasExploded;

	private readonly int levelBombCategory = 4;

	private readonly int levelBombID = 5001;

	private float bombExplosiveCharge;

	private float explosiveCharge;

	private readonly float radius = 7f;

	private readonly float power = 3600f;

	private readonly float torquePower = 100000f;

	private readonly float upPower = 0.25f;

	private float GuidedRocketToggle;

	private float LockTargetKey;

	private float SettingMenu;

	public float GroupFireKey;

	public float GroupFireRateSlider;

	public float AutoEjectToggle;

	public float rocket;

	private float NoSmokeToggle;

	private float TorqueSlider;

	private float StabilityToggle;

	private float ShowRadarToggle;

	private float ShowPredictionToggle;

	private float ProjectileSpeedSlider;

	private float DragSlider;

	private float ActiveGuideRocketSearchAngleSlider;

	private float RadarTypeMenu;

	private float ManualOverrideKey;

	public float SPTeamKey;

	private float ImpactFuzeToggle;

	private float ProximityFuzeToggle;

	private float ProximityFuzeRangeSlider;

	private float GuideDelaySlider;

	private float HighExploToggle;

	public override void SafeAwake()
	{
		this.SettingMenu = AddMenu("SettingType", 0, SingleInstance<LanguageManager>.Instance.CurrentLanguage.SettingType);
		this.RadarTypeMenu = AddMenu("Radar Type", 0, SingleInstance<LanguageManager>.Instance.CurrentLanguage.RadarType);
		this.ShowRadarToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ShowRadar, "ShowRadar", defaultValue: false);
		this.ImpactFuzeToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ImpactFuze, "ImpactFuze", defaultValue: false);
		this.ProximityFuzeToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ProximityFuze, "ProximityFuze", defaultValue: false);
		this.StabilityToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.RocketStability, "RocketStabilityOn", defaultValue: false);
		this.AutoEjectToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.AutoRelease, "AutoGrabberRelease", defaultValue: false);
		this.NoSmokeToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.NoSmoke, "NoSmoke", defaultValue: false);
		this.HighExploToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.HighExplo, "HighExplo", defaultValue: false);
		this.ShowPredictionToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ShowProjectileInterception, "ShowPrediction", defaultValue: false);
		this.GuidedRocketToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.TrackTarget, "TrackingRocket", defaultValue: false);
		this.ActiveGuideRocketSearchAngleSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.SearchAngle, "searchAngle", 60f, 0f, maxSearchAngleNormal);
		this.TorqueSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.TorqueOnRocket, "torqueOnRocket", 100f, 0f, 100f);
		this.GuideDelaySlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.GuideDelay, "guideDelay", 0f, 0f, 2f);
		this.ProximityFuzeRangeSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.CloseRange, "closeRange", 0f, 0f, 10f);
		this.GroupFireRateSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.GroupFireRate, "groupFireRate", 0.25f, 0.1f, 1f);
		this.ProjectileSpeedSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ProjectileSpeed, "CannonBallSpeed", 1f, 0.1f, 1000f);
		this.DragSlider = AddSlider("炮弹阻力", "CannonBallDrag", 0.2f, 0f, 1f);
		this.LockTargetKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.LockTarget, "lockTarget", (KeyCode)127);
		this.GroupFireKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.GroupedFire, "groupFire", (KeyCode)0);
		this.ManualOverrideKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ManualOverride, "ActiveSearchKey", (KeyCode)303);
		this.SPTeamKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.SinglePlayerTeam, "SinglePlayerTeam", (KeyCode)0);
		this.rocket = ((Component)this).gameObject.GetComponent<TimedRocket>();
		rocketRigidbody = ((Component)this).gameObject.GetComponent<Rigidbody>();
	}

	public override void DisplayInMapper(bool value)
	{
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Invalid comparison between Unknown and I4
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Invalid comparison between Unknown and I4
		base.DisplayInMapper(value);
		bool flag = value && this.GuidedRocketToggle.IsActive;
		bool flag2 = flag && this.SettingMenu.Value == 1;
		bool flag3 = flag2 && this.RadarTypeMenu.Value == 0;
		bool flag4 = flag && this.SettingMenu.Value == 0;
		bool flag5 = (this.GuidedRocketToggle.IsActive ? flag4 : value);
		bool displayInMapper = !EnhancementToggle.IsActive || flag5;
		((MapperType)this.GuidedRocketToggle).DisplayInMapper = value;
		((MapperType)this.SettingMenu).DisplayInMapper = flag;
		((MapperType)this.SPTeamKey).DisplayInMapper = flag2 && (!StatMaster.isMP || Playerlist.Players.Count == 1);
		((MapperType)this.RadarTypeMenu).DisplayInMapper = flag2;
		((MapperType)this.TorqueSlider).DisplayInMapper = flag2;
		((MapperType)this.GuideDelaySlider).DisplayInMapper = flag2;
		((MapperType)this.ManualOverrideKey).DisplayInMapper = flag3;
		((MapperType)this.ShowRadarToggle).DisplayInMapper = flag3;
		((MapperType)this.ActiveGuideRocketSearchAngleSlider).DisplayInMapper = flag3;
		((MapperType)this.LockTargetKey).DisplayInMapper = flag3;
		((MapperType)this.ShowPredictionToggle).DisplayInMapper = flag3;
		((MapperType)this.ProjectileSpeedSlider).DisplayInMapper = flag3 && this.ShowPredictionToggle.IsActive;
		((MapperType)this.DragSlider).DisplayInMapper = false;
		((MapperType)this.StabilityToggle).DisplayInMapper = flag4;
		((MapperType)this.ImpactFuzeToggle).DisplayInMapper = flag4;
		((MapperType)this.ProximityFuzeToggle).DisplayInMapper = flag4;
		((MapperType)this.ProximityFuzeRangeSlider).DisplayInMapper = flag4 && this.ProximityFuzeToggle.IsActive;
		((MapperType)this.AutoEjectToggle).DisplayInMapper = flag5 && (int)this.GroupFireKey.GetKey(0) > 0;
		((MapperType)this.GroupFireKey).DisplayInMapper = flag5;
		((MapperType)this.GroupFireRateSlider).DisplayInMapper = flag5 && (int)this.GroupFireKey.GetKey(0) > 0;
		((MapperType)this.NoSmokeToggle).DisplayInMapper = flag5;
		((MapperType)this.HighExploToggle).DisplayInMapper = flag5;
		((MapperType)this.rocket.LaunchKey).DisplayInMapper = displayInMapper;
		((MapperType)this.rocket.DelaySlider).DisplayInMapper = displayInMapper;
		((MapperType)this.rocket.ChargeSlider).DisplayInMapper = displayInMapper;
		((MapperType)this.rocket.PowerSlider).DisplayInMapper = displayInMapper;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Expected O, but got Unknown
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		rocketInBuildSent = (removedFromGroup = false);
		explosiveCharge = (bombExplosiveCharge = this.rocket.ChargeSlider.Value);
		if (this.HighExploToggle.IsActive && !EnhancementBlock.EnhanceMore)
		{
			bombExplosiveCharge = Mathf.Clamp(explosiveCharge, 0f, 1.5f);
		}
		if (this.GuidedRocketToggle.IsActive)
		{
			launchTimeRecorded = (bombHasExploded = (rocketExploMsgSent = false));
			float num = Mathf.Clamp(this.ActiveGuideRocketSearchAngleSlider.Value, 0f, EnhancementBlock.EnhanceMore ? maxSearchAngleNo8 : maxSearchAngleNormal);
			float num2 = (EnhancementBlock.EnhanceMore ? 5000f : 2000f);
			radarObject = new GameObject("RocketRadar");
			radarObject.transform.SetParent(((Component)this.rocket).transform);
			radarObject.transform.position = ((Component)this).transform.position;
			radarObject.transform.rotation = ((Component)this).transform.rotation;
			radarObject.transform.localPosition = Vector3.forward * 0.5f;
			radarObject.transform.localScale = restoreScale(((Component)this.rocket).transform.localScale);
			radar = radarObject.GetComponent<RadarScript>() ?? radarObject.AddComponent<RadarScript>();
			_ = radar;
			_ = base.BB;
			_ = rocketRigidbody;
			_ = this.RadarTypeMenu.Value;
			_ = this.ShowRadarToggle.IsActive;
			_ = 30f;
			/*Error near IL_01ba: Invalid metadata token*/;
		}
		smokeTrail = null;
		if (this.NoSmokeToggle.IsActive)
		{
			ParticleSystem[] trail = this.rocket.trail;
			for (int i = 0; i < trail.Length; i++)
			{
				EmissionModule emission = trail[i].emission;
				((EmissionModule)(ref emission)).enabled = false;
			}
		}
		else
		{
			ParticleSystem[] trail = this.rocket.trail;
			foreach (ParticleSystem val in trail)
			{
				if (((Object)val).name.ToLower() == "smoketrail")
				{
					smokeTrail = val;
					ColorOverLifetimeModule colorOverLifetime = smokeTrail.colorOverLifetime;
					Gradient val2 = new Gradient();
					val2.alphaKeys = (GradientAlphaKey[])(object)new GradientAlphaKey[5]
					{
						new GradientAlphaKey(0f, 0f),
						new GradientAlphaKey(0.5f, 0.01f),
						new GradientAlphaKey(trailSmokePropertise.StartAlpha, trailSmokePropertise.StartAlphaTime),
						new GradientAlphaKey(trailSmokePropertise.EndAlpha, trailSmokePropertise.EndAlphaTime),
						new GradientAlphaKey(0f, 0.8f)
					};
					val2.colorKeys = (GradientColorKey[])(object)new GradientColorKey[4]
					{
						new GradientColorKey(new Color(1f, 1f, 0f, 1f), 0f),
						new GradientColorKey(new Color(0.882f, 0.365f, 0.176f, 1f), 0.019f),
						new GradientColorKey(trailSmokePropertise.StartColor, trailSmokePropertise.StartColorTime),
						new GradientColorKey(trailSmokePropertise.EndColor, trailSmokePropertise.EndColorTime)
					};
					((ColorOverLifetimeModule)(ref colorOverLifetime)).color = new MinMaxGradient(val2);
					break;
				}
			}
		}
		if ((int)this.GroupFireKey.GetKey(0) != 0 && !this.GroupFireKey.Ignored)
		{
			if (!SingleInstance<RocketsController>.Instance.playerGroupedRockets.ContainsKey(((BasicInfo)this.rocket).ParentMachine.PlayerID))
			{
				SingleInstance<RocketsController>.Instance.playerGroupedRockets.Add(((BasicInfo)this.rocket).ParentMachine.PlayerID, new Dictionary<KeyCode, HashSet<TimedRocket>>());
			}
			if (!SingleInstance<RocketsController>.Instance.playerGroupedRockets[((BasicInfo)this.rocket).ParentMachine.PlayerID].ContainsKey(this.GroupFireKey.GetKey(0)))
			{
				SingleInstance<RocketsController>.Instance.playerGroupedRockets[((BasicInfo)this.rocket).ParentMachine.PlayerID].Add(this.GroupFireKey.GetKey(0), new HashSet<TimedRocket>());
			}
			if (!SingleInstance<RocketsController>.Instance.playerGroupedRockets[((BasicInfo)this.rocket).ParentMachine.PlayerID][this.GroupFireKey.GetKey(0)].Contains(this.rocket))
			{
				SingleInstance<RocketsController>.Instance.playerGroupedRockets[((BasicInfo)this.rocket).ParentMachine.PlayerID][this.GroupFireKey.GetKey(0)].Add(this.rocket);
			}
		}
		static Vector3 restoreScale(Vector3 rocketScale)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			float num3 = 1f / rocketScale.x;
			float num4 = 1f / rocketScale.y;
			float num5 = 1f / rocketScale.z;
			return new Vector3(num3, num4, num5);
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		if (!((Component)this).gameObject.activeInHierarchy)
		{
			return;
		}
		if (!this.GroupFireKey.Ignored && (this.GroupFireKey.IsHeld || this.GroupFireKey.EmulationHeld(false)) && !StatMaster.isClient && !SingleInstance<RocketsController>.Instance.launchStarted)
		{
			((MonoBehaviour)this).StartCoroutine(SingleInstance<RocketsController>.Instance.LaunchRocketFromGroup(((BasicInfo)this.rocket).ParentMachine.PlayerID, this.GroupFireKey.GetKey(0)));
		}
		if ((Object)(object)radar != (Object)null)
		{
			if (!StatMaster.isClient)
			{
				_ = radar;
				_ = this.rocket.hasFired;
				/*Error near IL_00a1: Invalid metadata token*/;
			}
			if (this.GuidedRocketToggle.IsActive)
			{
				if (this.ManualOverrideKey.IsReleased || this.ManualOverrideKey.EmulationReleased())
				{
					_ = radar;
					/*Error near IL_00d6: Invalid metadata token*/;
				}
				if (this.LockTargetKey.IsPressed || this.LockTargetKey.EmulationPressed())
				{
					_ = radar;
					/*Error near IL_0162: Invalid metadata token*/;
				}
			}
		}
		if (this.rocket.hasFired)
		{
			SendRocketFired();
			if (!this.rocket.hasExploded && this.GuidedRocketToggle.IsActive)
			{
				guideController.enableAerodynamicEffect = this.StabilityToggle.IsActive;
				if (!launchTimeRecorded)
				{
					launchTimeRecorded = true;
					launchTime = Time.time;
				}
				if (Time.time - launchTime >= this.GuideDelaySlider.Value + 0.15f && this.TorqueSlider.Value > 0f)
				{
					guideController.Switch = true;
				}
				if (this.ProximityFuzeToggle.IsActive)
				{
					_ = radar;
					/*Error near IL_0245: Invalid metadata token*/;
				}
			}
		}
		if (this.rocket.hasExploded && !rocketExploMsgSent)
		{
			Object.Destroy((Object)(object)radarObject);
			Object.Destroy((Object)(object)guideObject);
			if (this.HighExploToggle.IsActive)
			{
				((MonoBehaviour)this).StartCoroutine(RocketExplode());
			}
			try
			{
				if (SingleInstance<RocketsController>.Instance.playerGroupedRockets.TryGetValue(StatMaster.isMP ? ((BasicInfo)this.rocket).ParentMachine.PlayerID : 0, out var value) && value.TryGetValue(this.GroupFireKey.GetKey(0), out var value2))
				{
					value2.Remove(this.rocket);
				}
			}
			catch
			{
			}
			rocketExploMsgSent = true;
		}
		if (!this.NoSmokeToggle.IsActive)
		{
			EmissionModule emission = smokeTrail.emission;
			MinMaxCurve rate = ((EmissionModule)(ref emission)).rate;
			((MinMaxCurve)(ref rate)).constant = trailSmokePropertise.EmissionConstant;
			((EmissionModule)(ref emission)).rate = rate;
			smokeTrail.startLifetime = trailSmokePropertise.Lifetime;
			smokeTrail.startSize = trailSmokePropertise.Size;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (EnhancementToggle.IsActive && this.rocket.hasFired && this.rocket.PowerSlider.Value > 0.1f)
		{
			Vector3 impulse = collision.impulse;
			if (((Vector3)(ref impulse)).magnitude / Time.fixedDeltaTime >= (this.ImpactFuzeToggle.IsActive ? triggerForceImpactFuzeOn : triggerForceImpactFuzeOff) || ((Object)collision.gameObject).name.Contains("CanonBall"))
			{
				((MonoBehaviour)this).StartCoroutine(RocketExplode());
			}
		}
	}

	private IEnumerator RocketExplode()
	{
		Vector3 position = ((Component)this.rocket).transform.position;
		Quaternion rotation = ((Component)this.rocket).transform.rotation;
		if (!this.rocket.hasExploded)
		{
			this.rocket.ExplodeMessage();
		}
		if (!this.HighExploToggle.IsActive || bombHasExploded || explosiveCharge == 0f)
		{
			yield break;
		}
		if (StatMaster.isHosting)
		{
			SendExplosionPositionToAll(position);
		}
		bombHasExploded = true;
		try
		{
			GameObject val = (GameObject)Object.Instantiate((Object)(object)((Component)PrefabMaster.LevelPrefabs[levelBombCategory].GetValue(levelBombID)).gameObject, position, rotation);
			ExplodeOnCollide component = val.GetComponent<ExplodeOnCollide>();
			val.transform.localScale = Vector3.one * bombExplosiveCharge;
			component.radius = radius * bombExplosiveCharge;
			component.power = power * bombExplosiveCharge;
			component.torquePower = torquePower * bombExplosiveCharge;
			component.upPower = upPower;
			component.Explodey();
		}
		catch
		{
		}
		Collider[] array = Physics.OverlapSphere(((Component)this.rocket).transform.position, radius * bombExplosiveCharge, LayerMask.op_Implicit(Game.BlockEntityLayerMask));
		int index = 0;
		int rank = 60;
		if (array.Length == 0)
		{
			yield break;
		}
		Collider[] array2 = array;
		foreach (Collider hit in array2)
		{
			if (index > rank)
			{
				index = 0;
				yield return 0;
			}
			if ((Object)(object)hit.attachedRigidbody != (Object)null && Object.op_Implicit((Object)(object)hit.attachedRigidbody) && (Object)(object)hit.attachedRigidbody != (Object)(object)((BasicInfo)this.rocket).Rigidbody && ((Component)hit.attachedRigidbody).gameObject.layer != 20 && ((Component)hit.attachedRigidbody).gameObject.layer != 22 && ((Component)hit.attachedRigidbody).tag != "KeepConstraintsAlways")
			{
				_ = ((Component)hit.attachedRigidbody).gameObject.layer;
				/*Error near IL_0290: Invalid metadata token*/;
			}
		}
	}

	public void SendRocketFired()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (removedFromGroup)
		{
			return;
		}
		if (StatMaster.isHosting)
		{
			Message val = Messages.rocketFiredMsg.CreateMessage(new object[1] { base.BB });
			ModNetworking.SendTo(Player.GetAllPlayers().Find((Player player) => player.NetworkId == ((BasicInfo)this.rocket).ParentMachine.PlayerID), val);
		}
		if (SingleInstance<RocketsController>.Instance.playerGroupedRockets.TryGetValue(StatMaster.isMP ? ((BasicInfo)this.rocket).ParentMachine.PlayerID : 0, out var value) && value.TryGetValue(this.GroupFireKey.GetKey(0), out var value2))
		{
			value2.Remove(this.rocket);
		}
		removedFromGroup = true;
	}

	private void SendExplosionPositionToAll(Vector3 position)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (StatMaster.isHosting)
		{
			ModNetworking.SendToAll(Messages.rocketHighExploPosition.CreateMessage(new object[2] { position, bombExplosiveCharge }));
		}
	}

	static RocketScript()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		trailSmokePropertise = new TrailSmokePropertise
		{
			EmissionConstant = BlockEnhancementMod.ModSetting.RocketSmokeEmissionConstant,
			Lifetime = BlockEnhancementMod.ModSetting.RocketSmokeLifetime,
			Size = BlockEnhancementMod.ModSetting.RocketSmokeSize,
			StartColor = BlockEnhancementMod.ModSetting.RocketSmokeStartColor,
			EndColor = BlockEnhancementMod.ModSetting.RocketSmokeEndColor,
			StartColorTime = BlockEnhancementMod.ModSetting.RocketSmokeStartColorTime,
			EndColorTime = BlockEnhancementMod.ModSetting.RocketSmokeEndColorTime,
			StartAlpha = BlockEnhancementMod.ModSetting.RocketSmokeStartAlpha,
			EndAlpha = BlockEnhancementMod.ModSetting.RocketSmokeEndAlpha,
			StartAlphaTime = BlockEnhancementMod.ModSetting.RocketSmokeStartAlphaTime,
			EndAlphaTime = BlockEnhancementMod.ModSetting.RocketSmokeEndAlphaTime
		};
	}
}
