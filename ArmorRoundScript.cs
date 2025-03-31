using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Modding;
using UnityEngine;

namespace BlockEnhancementMod;

internal class ArmorRoundScript : EnhancementBlock
{
	private MMenu directoryMenu;

	private MMenu fileMenu;

	private MToggle onCollisionToggle;

	private MToggle oneShotToggle;

	private MToggle loopToggle;

	private MToggle releaseToPauseToggle;

	private MToggle releaseToStopToggle;

	private MSlider volumeSlider;

	private MSlider pitchSlider;

	private MSlider distanceSlider;

	private MSlider dopplerSlider;

	private MSlider spatialBlendSlider;

	private MKey addVolumeKey;

	private MKey reduceVolumeKey;

	private MKey playKey;

	private MKey muteKey;

	private MKey stopKey;

	private MKey nextKey;

	private MKey lastKey;

	private AudioSource audioSource;

	private Dictionary<string, List<string>> audioClipDic;

	public static float GlobleAudioPitchValue = 1f;

	public override void SafeAwake()
	{
		directoryMenu = AddMenu("Directory", 0, new List<string> { "" });
		fileMenu = AddMenu("File", 0, new List<string> { "" });
		refeshMenu();
		playKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Play, "Play", (KeyCode)112);
		stopKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Stop, "Stop", (KeyCode)99);
		muteKey = AddKey(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Mute, "Mute", (KeyCode)109);
		loopToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Loop, "Loop", defaultValue: false);
		oneShotToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.OneShot, "One Shot", defaultValue: false);
		releaseToPauseToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ReleaseToPause, "Release To Pause", defaultValue: false);
		releaseToStopToggle = AddToggle(SingleInstance<LanguageManager>.Instance.CurrentLanguage.ReleaseToStop, "Release To Stop", defaultValue: false);
		volumeSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Volume, "Volume", 1f, 0f, 1f);
		pitchSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Pitch, "Pitch", 1f, 0f, 5f);
		distanceSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Distance, "Distance", 5f, 0f, 10f);
		dopplerSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Doppler, "Doppler", 1f, 0f, 5f);
		spatialBlendSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.SpatialBlend, "Spatial Blend", 1f, 0f, 1f);
		audioSource = ((Component)((Component)this).transform).gameObject.GetComponent<AudioSource>() ?? ((Component)((Component)this).transform).gameObject.AddComponent<AudioSource>();
		SingleInstance<AssetManager>.Instance.OnReread += refeshMenu;
	}

	public override void DisplayInMapper(bool value)
	{
		MMenu obj = directoryMenu;
		bool displayInMapper = (((MapperType)fileMenu).DisplayInMapper = value);
		((MapperType)obj).DisplayInMapper = displayInMapper;
		MKey obj2 = playKey;
		MKey obj3 = stopKey;
		bool flag3 = (((MapperType)muteKey).DisplayInMapper = value);
		displayInMapper = (((MapperType)obj3).DisplayInMapper = flag3);
		((MapperType)obj2).DisplayInMapper = displayInMapper;
		((MapperType)oneShotToggle).DisplayInMapper = !loopToggle.IsActive && !releaseToPauseToggle.IsActive && !releaseToStopToggle.IsActive && value;
		((MapperType)loopToggle).DisplayInMapper = !oneShotToggle.IsActive && value;
		((MapperType)releaseToPauseToggle).DisplayInMapper = !oneShotToggle.IsActive && !releaseToStopToggle.IsActive && value;
		((MapperType)releaseToStopToggle).DisplayInMapper = !oneShotToggle.IsActive && !releaseToPauseToggle.IsActive && value;
		MSlider obj4 = volumeSlider;
		displayInMapper = (((MapperType)pitchSlider).DisplayInMapper = value);
		((MapperType)obj4).DisplayInMapper = displayInMapper;
		MSlider obj5 = distanceSlider;
		MSlider obj6 = dopplerSlider;
		flag3 = (((MapperType)spatialBlendSlider).DisplayInMapper = value);
		displayInMapper = (((MapperType)obj6).DisplayInMapper = flag3);
		((MapperType)obj5).DisplayInMapper = displayInMapper;
	}

	public override void ChangedProperties(MapperType mapper)
	{
		if (audioClipDic.Keys.Count == 0)
		{
			((MapperType)EnhancementToggle).DisplayInMapper = false;
			EnhancementToggle.IsActive = false;
			DisplayInMapper(value: false);
		}
		else if (mapper.Key == ((MapperType)directoryMenu).Key)
		{
			string text = audioClipDic.Keys.ToList()[directoryMenu.Value];
			List<string> list = formatList(audioClipDic[text]);
			((MapperType)directoryMenu).DisplayInMapper = true;
			fileMenu.Items = formatList(list, extention: true, text);
			fileMenu.Value = 0;
			((MapperType)fileMenu).DisplayInMapper = false;
			((MapperType)fileMenu).DisplayInMapper = true;
		}
	}

	public override void OnSimulateStart_EnhancementEnabled()
	{
		ModAudioClip ac = loadAudioClip(directoryMenu.Value, fileMenu.Value);
		((ModResource)ac).OnLoad += delegate
		{
			audioSource.clip = ModAudioClip.op_Implicit(ac);
		};
		audioSource.loop = loopToggle.IsActive;
		audioSource.pitch = pitchSlider.Value * GlobleAudioPitchValue;
		audioSource.volume = volumeSlider.Value;
		audioSource.spatialBlend = spatialBlendSlider.Value;
		audioSource.minDistance = distanceSlider.Value;
		audioSource.maxDistance = distanceSlider.Value * 3f;
		audioSource.dopplerLevel = dopplerSlider.Value;
	}

	public override void SimulateUpdateAlways_EnhancementEnable()
	{
		if (playKey.IsPressed || playKey.EmulationPressed())
		{
			if (!oneShotToggle.IsActive)
			{
				if (audioSource.time == 0f && !audioSource.isPlaying)
				{
					audioSource.Play();
				}
				else if (audioSource.time > 0f && audioSource.isPlaying)
				{
					audioSource.Pause();
				}
				else if (audioSource.time > 0f && !audioSource.isPlaying)
				{
					audioSource.UnPause();
				}
			}
			else
			{
				audioSource.PlayOneShot(audioSource.clip);
			}
		}
		if (stopKey.IsPressed || stopKey.EmulationPressed())
		{
			audioSource.Stop();
		}
		if (muteKey.IsPressed || muteKey.EmulationPressed())
		{
			audioSource.mute = !audioSource.mute;
		}
		if (playKey.IsReleased)
		{
			if (releaseToPauseToggle.IsActive)
			{
				audioSource.Pause();
			}
			if (releaseToStopToggle.IsActive)
			{
				audioSource.Stop();
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
	}

	private void refeshMenu()
	{
		audioClipDic = new Dictionary<string, List<string>>(SingleInstance<AssetManager>.Instance.AudioClipDic);
		bool flag = audioClipDic.Keys.Count == 0;
		directoryMenu.Items = ((!flag) ? formatList(audioClipDic.Keys.ToList()) : new List<string> { "" });
		directoryMenu.Value = 0;
		fileMenu.Items = ((!flag) ? formatList(audioClipDic[audioClipDic.Keys.ToList()[0]], extention: true) : new List<string> { "" });
		fileMenu.Value = 0;
		if (EnhancementToggle != null)
		{
			if (!flag)
			{
				((MapperType)EnhancementToggle).DisplayInMapper = true;
				DisplayInMapper(EnhancementToggle.IsActive);
			}
			else
			{
				((MapperType)EnhancementToggle).DisplayInMapper = false;
				EnhancementToggle.IsActive = false;
				DisplayInMapper(value: false);
			}
		}
	}

	private ModAudioClip loadAudioClip(int index_directoryMenu, int index_fileMenu)
	{
		string key = SingleInstance<AssetManager>.Instance.AudioClipDic.Keys.ToList()[index_directoryMenu];
		string path = SingleInstance<AssetManager>.Instance.AudioClipDic[key][index_fileMenu];
		string name = fileMenu.Items[index_fileMenu] + ExtensionMethods.GetRandomString();
		return SingleInstance<AssetManager>.Instance.LoadModAudioClip(name, path, data: true);
	}

	private List<string> formatList(List<string> list, bool extention = false, string path = "Audio Clips/")
	{
		List<string> list2 = new List<string>();
		foreach (string item in list)
		{
			string text = Regex.Replace(item, path, "");
			if (extention)
			{
				try
				{
					text = text.Substring(1, text.Length - 4);
				}
				catch
				{
				}
			}
			list2.Add(text);
		}
		return list2;
	}
}
