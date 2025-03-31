using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Modding;
using UnityEngine;

namespace BlockEnhancementMod;

public class BlockEnhancementMod : ModEntryPoint
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Action<string[]> _003C_003E9__9_1;

		public static Action<string[]> _003C_003E9__9_2;

		public static Action<string[]> _003C_003E9__9_3;

		public static Action<string[]> _003C_003E9__9_4;

		public static Action<string[]> _003C_003E9__9_5;

		public static Action<string[]> _003C_003E9__9_6;

		public static Action<string[]> _003C_003E9__9_7;

		public static Action<string[]> _003C_003E9__9_8;

		public static Action<string[]> _003C_003E9__9_9;

		public static Action<string[]> _003C_003E9__9_10;

		public static Action<string[]> _003C_003E9__9_11;

		public static Action<string[]> _003C_003E9__9_12;

		public static Action<string[]> _003C_003E9__9_13;

		public static Action<string[]> _003C_003E9__9_14;

		public static CommandHandler _003C_003E9__9_0;

		internal void _003COnLoad_003Eb__9_0(string[] value)
		{
			Dictionary<string, Action<string[]>> dictionary = new Dictionary<string, Action<string[]>>
			{
				{
					"srsecon",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeEmissionConstant = float.Parse(args[1]);
					}
				},
				{
					"srsl",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeLifetime = float.Parse(args[1]);
					}
				},
				{
					"srss",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeSize = float.Parse(args[1]);
					}
				},
				{
					"srssc",
					delegate(string[] args)
					{
						//IL_001d: Unknown result type (might be due to invalid IL or missing references)
						ModSetting.RocketSmokeStartColor = new Color(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
					}
				},
				{
					"srsec",
					delegate(string[] args)
					{
						//IL_001d: Unknown result type (might be due to invalid IL or missing references)
						ModSetting.RocketSmokeEndColor = new Color(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
					}
				},
				{
					"srssct",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeStartColorTime = float.Parse(args[1]);
					}
				},
				{
					"srsect",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeEndColorTime = float.Parse(args[1]);
					}
				},
				{
					"srssa",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeStartAlpha = float.Parse(args[1]);
					}
				},
				{
					"srsea",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeEndAlpha = float.Parse(args[1]);
					}
				},
				{
					"srssat",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeStartAlphaTime = float.Parse(args[1]);
					}
				},
				{
					"srseat",
					delegate(string[] args)
					{
						ModSetting.RocketSmokeEndAlphaTime = float.Parse(args[1]);
					}
				},
				{
					"srf",
					delegate(string[] args)
					{
						ModSetting.RadarFrequency = int.Parse(args[1]);
					}
				},
				{
					"rfa",
					delegate
					{
						SingleInstance<AssetManager>.Instance.RereadAudioClipAsset();
					}
				},
				{
					"setgap",
					delegate(string[] args)
					{
						ArmorRoundScript.GlobleAudioPitchValue = Mathf.Clamp(float.Parse(args[1]), 0f, 2f);
					}
				}
			};
			try
			{
				if (dictionary.ContainsKey(value[0].ToLower()))
				{
					dictionary[value[0].ToLower()](value);
				}
				else
				{
					Debug.Log((object)$"Unknown command '{value[0]}', type 'help' for list.");
				}
			}
			catch (Exception ex)
			{
				Debug.Log((object)ex.Message);
				Debug.Log((object)$"Unknown command '{value[0]}', type 'help' for list.");
			}
		}

		internal void _003COnLoad_003Eb__9_1(string[] args)
		{
			ModSetting.RocketSmokeEmissionConstant = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_2(string[] args)
		{
			ModSetting.RocketSmokeLifetime = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_3(string[] args)
		{
			ModSetting.RocketSmokeSize = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_4(string[] args)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			ModSetting.RocketSmokeStartColor = new Color(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
		}

		internal void _003COnLoad_003Eb__9_5(string[] args)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			ModSetting.RocketSmokeEndColor = new Color(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
		}

		internal void _003COnLoad_003Eb__9_6(string[] args)
		{
			ModSetting.RocketSmokeStartColorTime = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_7(string[] args)
		{
			ModSetting.RocketSmokeEndColorTime = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_8(string[] args)
		{
			ModSetting.RocketSmokeStartAlpha = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_9(string[] args)
		{
			ModSetting.RocketSmokeEndAlpha = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_10(string[] args)
		{
			ModSetting.RocketSmokeStartAlphaTime = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_11(string[] args)
		{
			ModSetting.RocketSmokeEndAlphaTime = float.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_12(string[] args)
		{
			ModSetting.RadarFrequency = int.Parse(args[1]);
		}

		internal void _003COnLoad_003Eb__9_13(string[] args)
		{
			SingleInstance<AssetManager>.Instance.RereadAudioClipAsset();
		}

		internal void _003COnLoad_003Eb__9_14(string[] args)
		{
			ArmorRoundScript.GlobleAudioPitchValue = Mathf.Clamp(float.Parse(args[1]), 0f, 2f);
		}
	}

	public static GameObject mod;

	public static Configuration Configuration { get; private set; }

	public static ModSetting ModSetting { get; internal set; }

	public override void OnLoad()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		Configuration = Configuration.FormatXDataToConfig();
		ModSetting = new ModSetting();
		mod = new GameObject("Block Enhancement Mod");
		Object.DontDestroyOnLoad((Object)(object)mod);
		mod.AddComponent<EnhancementBlockController>();
		mod.AddComponent<ModSettingUI>();
		((Component)SingleInstance<LanguageManager>.Instance).transform.SetParent(mod.transform);
		((Component)SingleInstance<MessageController>.Instance).transform.SetParent(mod.transform);
		((Component)SingleInstance<RocketsController>.Instance).transform.SetParent(mod.transform);
		((Component)SingleInstance<AssetManager>.Instance).transform.SetParent(mod.transform);
		object obj = _003C_003Ec._003C_003E9__9_0;
		if (obj == null)
		{
			CommandHandler val = delegate(string[] value)
			{
				Dictionary<string, Action<string[]>> dictionary = new Dictionary<string, Action<string[]>>
				{
					{
						"srsecon",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeEmissionConstant = float.Parse(args[1]);
						}
					},
					{
						"srsl",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeLifetime = float.Parse(args[1]);
						}
					},
					{
						"srss",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeSize = float.Parse(args[1]);
						}
					},
					{
						"srssc",
						delegate(string[] args)
						{
							//IL_001d: Unknown result type (might be due to invalid IL or missing references)
							ModSetting.RocketSmokeStartColor = new Color(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
						}
					},
					{
						"srsec",
						delegate(string[] args)
						{
							//IL_001d: Unknown result type (might be due to invalid IL or missing references)
							ModSetting.RocketSmokeEndColor = new Color(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
						}
					},
					{
						"srssct",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeStartColorTime = float.Parse(args[1]);
						}
					},
					{
						"srsect",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeEndColorTime = float.Parse(args[1]);
						}
					},
					{
						"srssa",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeStartAlpha = float.Parse(args[1]);
						}
					},
					{
						"srsea",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeEndAlpha = float.Parse(args[1]);
						}
					},
					{
						"srssat",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeStartAlphaTime = float.Parse(args[1]);
						}
					},
					{
						"srseat",
						delegate(string[] args)
						{
							ModSetting.RocketSmokeEndAlphaTime = float.Parse(args[1]);
						}
					},
					{
						"srf",
						delegate(string[] args)
						{
							ModSetting.RadarFrequency = int.Parse(args[1]);
						}
					},
					{
						"rfa",
						delegate
						{
							SingleInstance<AssetManager>.Instance.RereadAudioClipAsset();
						}
					},
					{
						"setgap",
						delegate(string[] args)
						{
							ArmorRoundScript.GlobleAudioPitchValue = Mathf.Clamp(float.Parse(args[1]), 0f, 2f);
						}
					}
				};
				try
				{
					if (dictionary.ContainsKey(value[0].ToLower()))
					{
						dictionary[value[0].ToLower()](value);
					}
					else
					{
						Debug.Log((object)$"Unknown command '{value[0]}', type 'help' for list.");
					}
				}
				catch (Exception ex)
				{
					Debug.Log((object)ex.Message);
					Debug.Log((object)$"Unknown command '{value[0]}', type 'help' for list.");
				}
			};
			_003C_003Ec._003C_003E9__9_0 = val;
			obj = (object)val;
		}
		ModConsole.RegisterCommand("be", (CommandHandler)obj, "<color=#FF6347>Enhancement Mod Commands\n  Usage: be srsecon :  set rocket smoke emission constant. ('be srsecon 80.0' )\n  Usage: be srsl :  set rocket smoke lifetime. ('be srsl 1.0' )\n  Usage: be srss :  set rocket smoke size. ('be srss 3.5' )\n  Usage: be srssc :  set rocket smoke start color. ('be srssc 0.1 0.14 0.15' )\n  Usage: be srsec :  set rocket smoke end color. ('be srsec 0.1 0.14 0.15' )\n  Usage: be srssct :  set rocket smoke start color time. ('be srssct 0.09' )\n  Usage: be srsect :  set rocket smoke end color time. ('be srsect 1' )\n  Usage: be srssa :  set rocket smoke start alpha. ('be srssa 1.0' )\n  Usage: be srsea :  set rocket smoke end alpha. ('be srsea 1.0' )\n  Usage: be srssat :  set rocket smoke start alpha time. ('be srssat 0.076' )\n  Usage: be srseat :  set rocket smoke end alpha time. ('be srseat 0.26' )\n  Usage: be rfa:  Refresh asset resource.\n  Usage: be setgap [value]:  Set Globle Audio Pitch Value(default value:1,interval:[0,2]).\n</color>");
	}
}
