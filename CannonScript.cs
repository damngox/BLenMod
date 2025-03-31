using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace BlockEnhancementMod.Blocks;

public class CannonScript : CanonBlock_GenericEnhanceScript
{
	public class Bullet
	{
		public GameObject bulletObject;

		public Rigidbody rigidbody;

		private TrailRenderer TrailRenderer;

		internal CanonBlock CB;

		public float Mass
		{
			get
			{
				return rigidbody.mass;
			}
			set
			{
				rigidbody.mass = Mathf.Clamp(value, 0.1f, value);
			}
		}

		public float Drag
		{
			get
			{
				return rigidbody.drag;
			}
			set
			{
				rigidbody.drag = value;
			}
		}

		public float DelayCollision { get; set; }

		public bool TrailEnable
		{
			get
			{
				return ((Renderer)TrailRenderer).enabled;
			}
			set
			{
				((Renderer)TrailRenderer).enabled = value;
			}
		}

		public float TrailLength
		{
			get
			{
				return TrailRenderer.time;
			}
			set
			{
				TrailRenderer.time = value;
			}
		}

		public Color TrailColor
		{
			get
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				return ((Renderer)TrailRenderer).material.color;
			}
			set
			{
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				((Renderer)TrailRenderer).material.SetColor("_TintColor", value);
			}
		}

		public bool Custom { get; set; }

		public bool InheritSize { get; set; }

		public Bullet(CanonBlock canonBlock)
		{
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Expected O, but got Unknown
			CB = canonBlock;
			bulletObject = Object.Instantiate<GameObject>(((Component)CB.boltObject).gameObject);
			bulletObject.SetActive(false);
			rigidbody = bulletObject.GetComponent<Rigidbody>();
			rigidbody.detectCollisions = false;
			bulletObject.GetComponent<Collider>().enabled = false;
			TrailRenderer = bulletObject.GetComponent<TrailRenderer>() ?? bulletObject.AddComponent<TrailRenderer>();
			TrailRenderer.autodestruct = false;
			((Renderer)TrailRenderer).receiveShadows = false;
			((Renderer)TrailRenderer).shadowCastingMode = (ShadowCastingMode)0;
			TrailRenderer trailRenderer = TrailRenderer;
			Vector3 localScale = bulletObject.transform.localScale;
			trailRenderer.startWidth = 0.5f * ((Vector3)(ref localScale)).magnitude;
			TrailRenderer.endWidth = 0.1f;
			((Renderer)TrailRenderer).material = new Material(Shader.Find("Particles/Additive"));
		}

		public void CreateCustomBullet()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			Transform transform = ((Component)CB).transform;
			bulletObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			((Component)CB.boltObject).gameObject.SetActive(false);
			if (InheritSize)
			{
				Vector3 val = Vector3.Scale(Vector3.one * Mathf.Min(transform.localScale.x, transform.localScale.z), new Vector3(0.5f, 0.5f, 0.5f));
				Transform transform2 = bulletObject.transform;
				Vector3 localScale = (((Component)CB.particles[0]).transform.localScale = val);
				transform2.localScale = localScale;
			}
		}
	}

	public MToggle BullerCustomBulletToggle;

	public MToggle BulletInheritSizeToggle;

	public MSlider BulletMassSlider;

	public MSlider BulletDragSlider;

	public MToggle BulletTrailToggle;

	public MSlider BulletTrailLengthSlider;

	public MSlider BulletDelayCollisionSlider;

	public MColourSlider BulletTrailColorSlider;

	public Bullet bullet;

	private bool lastInfinite;

	private bool firstShoot = true;

	public override void SafeAwake()
	{
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		base.SafeAwake();
		bullet = new Bullet(CB);
		BullerCustomBulletToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.CustomBullet, "Bullet", defaultValue: false);
		BulletInheritSizeToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.InheritSize, "InheritSize", defaultValue: false);
		BulletMassSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.BulletMass, "BulletMass", 2f, 0.1f, 2f);
		BulletDragSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.BulletDrag, "BulletDrag", 0.2f, 0.01f, 0.5f);
		BulletDelayCollisionSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.BulletDelayCollision, "Delay Collision", 0.2f, 0f, 0.5f);
		BulletTrailToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Trail, "Trail", defaultValue: false);
		BulletTrailLengthSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.TrailLength, "trail length", 1f, 0.2f, 2f);
		BulletTrailColorSlider = AddColourSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.TrailColor, "trail color", Color.yellow, snapColors: false);
	}

	public override void DisplayInMapper(bool value)
	{
		base.DisplayInMapper(value);
		bool flag = StatMaster.IsLevelEditorOnly || !StatMaster.isMP;
		bool flag2 = flag && BullerCustomBulletToggle.IsActive;
		((MapperType)BullerCustomBulletToggle).DisplayInMapper = value && flag;
		((MapperType)BulletInheritSizeToggle).DisplayInMapper = value && flag2;
		((MapperType)BulletMassSlider).DisplayInMapper = value && flag2;
		((MapperType)BulletDragSlider).DisplayInMapper = value && flag2;
		((MapperType)BulletDelayCollisionSlider).DisplayInMapper = value && flag2;
		((MapperType)BulletTrailToggle).DisplayInMapper = value && flag2;
		((MapperType)BulletTrailColorSlider).DisplayInMapper = BulletTrailToggle.IsActive && flag2;
		((MapperType)BulletTrailLengthSlider).DisplayInMapper = BulletTrailToggle.IsActive && flag2;
	}

	public override void OnSimulateStartAlways()
	{
		base.OnSimulateStartAlways();
		lastInfinite = GodTools.InfiniteAmmoMode;
		BulletInit();
		if (StatMaster.isMP)
		{
			Bullet obj = bullet;
			bool custom = (bullet.TrailEnable = false);
			obj.Custom = custom;
		}
		if (bullet.Custom)
		{
			bullet.CreateCustomBullet();
		}
		if (base.EnhancementEnabled || !((Component)CB.boltObject).gameObject.activeSelf)
		{
			CB.randomDelay = 0f;
		}
		void BulletInit()
		{
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			bullet.Custom = BullerCustomBulletToggle.IsActive;
			bullet.Mass = BulletMassSlider.Value;
			bullet.Drag = BulletDragSlider.Value;
			bullet.DelayCollision = BulletDelayCollisionSlider.Value;
			bullet.InheritSize = BulletInheritSizeToggle.IsActive;
			bullet.TrailEnable = BulletTrailToggle.IsActive;
			bullet.TrailLength = BulletTrailLengthSlider.Value;
			bullet.TrailColor = BulletTrailColorSlider.Value;
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
	}

	public override void SimulateUpdateAlways()
	{
		base.SimulateUpdateAlways();
		if (StatMaster.isClient)
		{
			return;
		}
		if (CB.ShootKey.IsReleased || CB.ShootKey.EmulationReleased())
		{
			if (GodTools.InfiniteAmmoMode)
			{
				base.ShootEnabled = true;
			}
			((MonoBehaviour)this).StopCoroutine(Shoot());
			firstShoot = true;
		}
		if ((CB.ShootKey.IsHeld || CB.ShootKey.EmulationHeld(false)) && base.ShootEnabled)
		{
			((MonoBehaviour)this).StopCoroutine(Shoot());
			((MonoBehaviour)this).StartCoroutine(Shoot());
		}
		else
		{
			((MonoBehaviour)this).StopCoroutine(Shoot());
		}
		if (base.EnhancementEnabled && lastInfinite != GodTools.InfiniteAmmoMode)
		{
			lastInfinite = GodTools.InfiniteAmmoMode;
			if (lastInfinite)
			{
				base.ShootEnabled = true;
			}
		}
	}

	protected override IEnumerator Shoot()
	{
		base.ShootEnabled = false;
		float randomDelay = 0f;
		if (!((Component)CB.boltObject).gameObject.activeSelf)
		{
			if (base.EnhancementEnabled)
			{
				if (bullet.Custom)
				{
					((MonoBehaviour)this).StartCoroutine(shoot(bullet.bulletObject));
				}
				else
				{
					((MonoBehaviour)this).StartCoroutine(shoot(((Component)CB.boltObject).gameObject));
				}
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(shoot(((Component)CB.boltObject).gameObject));
			}
		}
		else if (base.EnhancementEnabled)
		{
			if (bullet.Custom)
			{
				((MonoBehaviour)this).StartCoroutine(shoot(bullet.bulletObject));
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(shoot());
			}
		}
		yield return (object)new WaitForSeconds(IntervalSlider.Value + randomDelay);
		if (GodTools.InfiniteAmmoMode && base.EnhancementEnabled)
		{
			base.ShootEnabled = true;
		}
		IEnumerator shoot(GameObject bulletObject = null)
		{
			randomDelay = Random.Range(0f, RandomDelaySlider.Value);
			yield return (object)new WaitForSeconds(randomDelay);
			if ((Object)(object)bulletObject != (Object)null)
			{
				GameObject val = (GameObject)Object.Instantiate((Object)(object)bulletObject, ((Component)this).transform.TransformPoint(CB.boltSpawnPos), CB.boltSpawnRot);
				val.AddComponent<DelayCollision>().Delay = bullet.DelayCollision;
				val.SetActive(true);
				val.GetComponent<Rigidbody>().AddForce(-((Component)this).transform.up * CB.boltSpeed * CB.StrengthSlider.Value);
			}
			if (!firstShoot)
			{
				CB.Shoot();
			}
			firstShoot = false;
		}
	}
}
