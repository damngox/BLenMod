using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlockEnhancementMod;

[Obsolete]
public class SkinManager
{
	public List<Mesh> meshes;

	public List<string> meshNames => ConvertToStrings(meshes);

	private List<string> ConvertToStrings(List<Mesh> meshes)
	{
		List<string> list = new List<string>();
		foreach (Mesh mesh in meshes)
		{
			list.Add(((Object)mesh).name);
		}
		return list;
	}
}
