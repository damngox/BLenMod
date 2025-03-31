using System;
using System.Collections.Generic;
using System.Linq;
using Modding;
using UnityEngine;

namespace BlockEnhancementMod;

public class AssetManager : SingleInstance<AssetManager>
{
	public bool Data = true;

	public readonly string AudioFileRootPath = "Audio Clips";

	public Dictionary<string, List<string>> AudioClipDic = new Dictionary<string, List<string>>();

	public override string Name { get; } = "Asset Manager";

	public event Action OnReread;

	private void Awake()
	{
		RereadAudioClipAsset();
	}

	private Dictionary<string, List<string>> readAudioClips()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
		List<string> list = new List<string> { ".ogg", ".wav" };
		List<string> list2 = ModIO.GetDirectories(AudioFileRootPath, Data).ToList();
		list2.Insert(0, "Audio Clips");
		foreach (string item in list2)
		{
			List<string> list3 = new List<string>();
			string[] files = ModIO.GetFiles(item, Data);
			foreach (string text in files)
			{
				if (list.Contains(PathHelper.GetExtension(text)))
				{
					list3.Add(text);
				}
			}
			if (list3.Count != 0)
			{
				dictionary.Add(item, list3);
			}
		}
		ExtensionMethods.ShowMessageWithColor("Audio Clip Asset Read Complete", Color.green);
		return dictionary;
	}

	public ModAudioClip LoadModAudioClip(string name, string path, bool data = false)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			return ModResource.CreateAudioClipResource(name, path, data);
		}
		catch (Exception ex)
		{
			ExtensionMethods.ShowMessageWithColor(ex.Message, Color.red);
			return null;
		}
	}

	public Mesh LoadFormObj2(string name, string path)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		string objText = ModIO.ReadAllText(Application.dataPath + "/Mods/BlockEnhancementMod/" + path, false);
		Mesh val = new Mesh();
		ObjMesh objMesh = new ObjMesh().LoadFromObj(objText);
		val.vertices = objMesh.VertexArray;
		val.normals = objMesh.NormalArray;
		val.triangles = objMesh.TriangleArray;
		val.uv = objMesh.UVArray;
		val.RecalculateBounds();
		return val;
	}

	public void RereadAudioClipAsset()
	{
		if (!ModIO.ExistsDirectory(AudioFileRootPath, Data))
		{
			ModIO.CreateDirectory(AudioFileRootPath, Data);
		}
		AudioClipDic = readAudioClips();
		this.OnReread?.Invoke();
	}
}
