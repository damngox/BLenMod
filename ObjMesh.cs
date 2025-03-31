using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace BlockEnhancementMod;

public class ObjMesh
{
	private List<Vector3> uvArrayList;

	private List<Vector3> normalArrayList;

	private List<Vector3> vertexArrayList;

	private List<Vector3> faceVertexNormalUV;

	public Vector2[] UVArray;

	public Vector3[] NormalArray;

	public Vector3[] VertexArray;

	public int[] TriangleArray;

	public ObjMesh()
	{
		uvArrayList = new List<Vector3>();
		normalArrayList = new List<Vector3>();
		vertexArrayList = new List<Vector3>();
		faceVertexNormalUV = new List<Vector3>();
	}

	public ObjMesh LoadFromObj(string objText)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		if (objText.Length <= 0)
		{
			return null;
		}
		objText = objText.Replace("  ", " ");
		string[] array = objText.Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(' ');
			switch (array2[0])
			{
			case "v":
				vertexArrayList.Add(new Vector3(ConvertToFloat(array2[1]), ConvertToFloat(array2[2]), ConvertToFloat(array2[3])));
				break;
			case "vn":
				normalArrayList.Add(new Vector3(ConvertToFloat(array2[1]), ConvertToFloat(array2[2]), ConvertToFloat(array2[3])));
				break;
			case "vt":
				uvArrayList.Add(new Vector3(ConvertToFloat(array2[1]), ConvertToFloat(array2[2])));
				break;
			case "f":
				GetTriangleList(array2);
				break;
			}
		}
		Combine();
		return this;
	}

	private void Combine()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		for (int i = 0; i < faceVertexNormalUV.Count; i++)
		{
			if (!(faceVertexNormalUV[i] != Vector3.zero))
			{
				continue;
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(i);
			for (int j = 0; j < faceVertexNormalUV.Count; j++)
			{
				if (faceVertexNormalUV[j] != Vector3.zero && i != j)
				{
					Vector3 val = faceVertexNormalUV[i];
					Vector3 val2 = faceVertexNormalUV[j];
					if (val.x == val2.x && val.y == val2.y)
					{
						arrayList.Add(j);
						faceVertexNormalUV[j] = Vector3.zero;
					}
				}
			}
			dictionary.Add(i, arrayList);
		}
		VertexArray = (Vector3[])(object)new Vector3[dictionary.Count];
		UVArray = (Vector2[])(object)new Vector2[dictionary.Count];
		NormalArray = (Vector3[])(object)new Vector3[dictionary.Count];
		TriangleArray = new int[faceVertexNormalUV.Count];
		int num = 0;
		foreach (KeyValuePair<int, ArrayList> item in dictionary)
		{
			foreach (int item2 in item.Value)
			{
				TriangleArray[item2] = num;
			}
			Vector3 val3 = faceVertexNormalUV[item.Key];
			VertexArray[num] = vertexArrayList[(int)val3.x - 1];
			if (uvArrayList.Count > 0)
			{
				Vector3 val4 = uvArrayList[(int)val3.y - 1];
				UVArray[num] = new Vector2(val4.x, val4.y);
			}
			if (normalArrayList.Count > 0)
			{
				NormalArray[num] = normalArrayList[(int)val3.z - 1];
			}
			num++;
		}
	}

	private void GetTriangleList(string[] chars)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		Vector3 item = default(Vector3);
		for (int i = 1; i < chars.Length; i++)
		{
			if (chars[i].Contains("/"))
			{
				string[] array = chars[i].Split('/');
				((Vector3)(ref item))._002Ector(0f, 0f);
				item.x = ConvertToInt(array[0]);
				if (array.Length > 1 && array[1] != "")
				{
					item.y = ConvertToInt(array[1]);
				}
				if (array.Length > 2 && array[2] != "")
				{
					item.z = ConvertToInt(array[2]);
				}
				list.Add(item);
			}
		}
		for (int j = 1; j < list.Count - 1; j++)
		{
			list2.Add(list[0]);
			list2.Add(list[j]);
			list2.Add(list[j + 1]);
		}
		foreach (Vector3 item2 in list2)
		{
			faceVertexNormalUV.Add(item2);
		}
	}

	private float ConvertToFloat(string s)
	{
		return (float)Convert.ToDouble(s, CultureInfo.InvariantCulture);
	}

	private int ConvertToInt(string s)
	{
		return Convert.ToInt32(s, CultureInfo.InvariantCulture);
	}
}
