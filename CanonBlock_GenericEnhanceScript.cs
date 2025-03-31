using System.Collections;
using Modding.Common;
using UnityEngine;

namespace BlockEnhancementMod;

public class CanonBlock_GenericEnhanceScript : EnhancementBlock
{
	public MSlider IntervalSlider;

	public MSlider RandomDelaySlider;

	public MSlider KnockBackSpeedSlider;

	public CanonBlock CB;

	public bool firstShotFired { get; protected set; } = true;

	public bool ShootEnabled { get; set; } = true;

	public override void SafeAwake()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		CB = ((Component)this).GetComponent<CanonBlock>();
		IntervalSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.FireInterval, "Interval", 0.25f, EnhancementBlock.EnhanceMore ? 0f : 0.1f, 0.5f);
		RandomDelaySlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.RandomDelay, "RandomDelay", 0.2f, 0f, 0.5f);
		KnockBackSpeedSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Recoil, "KnockBackSpeed", 1f, EnhancementBlock.EnhanceMore ? 0f : 0.25f, 1f);
		CB.StrengthSlider.ValueChanged += (ValueChangeHandler)delegate(float value)
		{
			if (!EnhancementBlock.EnhanceMore && StatMaster.isMP && Player.GetAllPlayers().Count > 1)
			{
				if (value > 20f)
				{
					((MapperType)KnockBackSpeedSlider).DisplayInMapper = false;
					KnockBackSpeedSlider.Value = 1f;
				}
				else
				{
					((MapperType)KnockBackSpeedSlider).DisplayInMapper = true;
				}
			}
		};
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)IntervalSlider).DisplayInMapper = value;
		((MapperType)RandomDelaySlider).DisplayInMapper = value;
		((MapperType)KnockBackSpeedSlider).DisplayInMapper = value;
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		if (!StatMaster.isClient)
		{
			CB.randomDelay = Random.Range(0f, RandomDelaySlider.Value);
			CanonBlock cB = CB;
			cB.knockbackSpeed *= Mathf.Clamp(KnockBackSpeedSlider.Value, KnockBackSpeedSlider.Min, KnockBackSpeedSlider.Max);
		}
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		if (!StatMaster.isClient)
		{
			if (CB.ShootKey.IsReleased)
			{
				firstShotFired = true;
				ShootEnabled = true;
				((MonoBehaviour)this).StopCoroutine(Shoot());
			}
			if (CB.ShootKey.IsHeld && ShootEnabled)
			{
				((MonoBehaviour)this).StartCoroutine(Shoot());
			}
		}
	}

	protected virtual IEnumerator Shoot()
	{
		ShootEnabled = false;
		if (GodTools.InfiniteAmmoMode && !firstShotFired)
		{
			CB.Shoot();
		}
		else
		{
			firstShotFired = false;
		}
		yield return (object)new WaitForSeconds(IntervalSlider.Value);
		ShootEnabled = true;
	}
}
