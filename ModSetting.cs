using UnityEngine;

namespace BlockEnhancementMod;

public class ModSetting
{
	public bool EnhanceMore
	{
		get
		{
			return getValue<bool>("Enhance More");
		}
		set
		{
			Changed("Enhance More", value);
		}
	}

	public bool ShowUI
	{
		get
		{
			return getValue<bool>("ShowUI");
		}
		set
		{
			Changed("ShowUI", value);
		}
	}

	public bool Friction
	{
		get
		{
			return getValue<bool>("Friction");
		}
		set
		{
			Changed("Friction", value);
		}
	}

	public bool DisplayWarning
	{
		get
		{
			return getValue<bool>("Display Warning");
		}
		set
		{
			Changed("Display Warning", value);
		}
	}

	public bool MarkTarget
	{
		get
		{
			return getValue<bool>("Mark Target");
		}
		set
		{
			Changed("Mark Target", value);
		}
	}

	public bool DisplayRocketCount
	{
		get
		{
			return getValue<bool>("Display Rocket Count");
		}
		set
		{
			Changed("Display Rocket Count", value);
		}
	}

	public bool BuildSurface_Collision_Mass
	{
		get
		{
			return getValue<bool>("BuildSurface");
		}
		set
		{
			Changed("BuildSurface", value);
		}
	}

	public float GuideControl_P_Factor
	{
		get
		{
			return getValue<float>("GuideControl P Factor");
		}
		set
		{
			GuideController.pFactor = value;
			Changed("GuideControl P Factor", value);
		}
	}

	public float GuideControl_I_Factor
	{
		get
		{
			return getValue<float>("GuideControl I Factor");
		}
		set
		{
			GuideController.iFactor = value;
			Changed("GuideControl I Factor", value);
		}
	}

	public float GuideControl_D_Factor
	{
		get
		{
			return getValue<float>("GuideControl D Factor");
		}
		set
		{
			GuideController.dFactor = value;
			Changed("GuideControl D Factor", value);
		}
	}

	public float RocketSmokeEmissionConstant
	{
		get
		{
			return getValue<float>("Rocket Smoke Emission Constant");
		}
		set
		{
			RocketScript.trailSmokePropertise.EmissionConstant = value;
			Changed("Rocket Smoke Emission Constant", value);
		}
	}

	public float RocketSmokeLifetime
	{
		get
		{
			return getValue<float>("Rocket Smoke Lifetime");
		}
		set
		{
			RocketScript.trailSmokePropertise.Lifetime = value;
			Changed("Rocket Smoke Lifetime", value);
		}
	}

	public float RocketSmokeSize
	{
		get
		{
			return getValue<float>("Rocket Smoke Size");
		}
		set
		{
			RocketScript.trailSmokePropertise.Size = value;
			Changed("Rocket Smoke Size", value);
		}
	}

	public Color RocketSmokeStartColor
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return getValue<Color>("Rocket Smoke Start Color");
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			RocketScript.trailSmokePropertise.StartColor = value;
			Changed<Color>("Rocket Smoke Start Color", value);
		}
	}

	public Color RocketSmokeEndColor
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return getValue<Color>("Rocket Smoke End Color");
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			RocketScript.trailSmokePropertise.EndColor = value;
			Changed<Color>("Rocket Smoke End Color", value);
		}
	}

	public float RocketSmokeStartColorTime
	{
		get
		{
			return getValue<float>("Rocket Smoke Start Color Time");
		}
		set
		{
			RocketScript.trailSmokePropertise.StartColorTime = value;
			Changed("Rocket Smoke Start Color Time", value);
		}
	}

	public float RocketSmokeEndColorTime
	{
		get
		{
			return getValue<float>("Rocket Smoke End Color Time");
		}
		set
		{
			RocketScript.trailSmokePropertise.EndColorTime = value;
			Changed("Rocket Smoke End Color Time", value);
		}
	}

	public float RocketSmokeStartAlpha
	{
		get
		{
			return getValue<float>("Rocket Smoke Start Alpha");
		}
		set
		{
			RocketScript.trailSmokePropertise.StartAlpha = value;
			Changed("Rocket Smoke Start Alpha", value);
		}
	}

	public float RocketSmokeEndAlpha
	{
		get
		{
			return getValue<float>("Rocket Smoke End Alpha");
		}
		set
		{
			RocketScript.trailSmokePropertise.EndAlpha = value;
			Changed("Rocket Smoke End Alpha", value);
		}
	}

	public float RocketSmokeStartAlphaTime
	{
		get
		{
			return getValue<float>("Rocket Smoke Start Alpha Time");
		}
		set
		{
			RocketScript.trailSmokePropertise.StartAlphaTime = value;
			Changed("Rocket Smoke Start Alpha Time", value);
		}
	}

	public float RocketSmokeEndAlphaTime
	{
		get
		{
			return getValue<float>("Rocket Smoke End Alpha Time");
		}
		set
		{
			RocketScript.trailSmokePropertise.EndAlphaTime = value;
			Changed("Rocket Smoke End Alpha Time", value);
		}
	}

	public int RadarFrequency
	{
		get
		{
			return getValue<int>("Radar Frequency");
		}
		set
		{
			/*Error near IL_0001: Invalid metadata token*/;
		}
	}

	private T getValue<T>(string key)
	{
		return BlockEnhancementMod.Configuration.GetValue<T>(key);
	}

	private void Changed<T>(string key, T value)
	{
		BlockEnhancementMod.Configuration.SetValue(key, value);
	}
}
