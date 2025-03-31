using System;
using UnityEngine;

namespace BlockEnhancementMod;

public static class ExtensionMethods
{
	public static T GetComponentInAll<T>(this Component component)
	{
		T val = component.GetComponentInChildren<T>();
		if (val == null)
		{
			val = component.GetComponentInParent<T>();
		}
		return val;
	}

	public static T GetComponentInAll<T>(this GameObject gameObject)
	{
		T val = gameObject.GetComponentInChildren<T>();
		if (val == null)
		{
			val = gameObject.GetComponentInParent<T>();
		}
		return val;
	}

	public static string SetColor(this string str, Color color)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		string arg = ColorUtility.ToHtmlStringRGB(color);
		return $"<color=#{arg}>{str}</color>";
	}

	public static string GetRandomString(int length = 4, bool useNum = true, bool useLow = true, bool useUpp = true, bool useSpe = false, string custom = "")
	{
		Random random = new Random(BitConverter.ToInt32(new byte[4]
		{
			(byte)Random.Range(0, 255),
			(byte)Random.Range(0, 255),
			(byte)Random.Range(0, 255),
			(byte)Random.Range(0, 255)
		}, 0));
		string text = null;
		string text2 = custom;
		if (useNum)
		{
			text2 += "0123456789";
		}
		if (useLow)
		{
			text2 += "abcdefghijklmnopqrstuvwxyz";
		}
		if (useUpp)
		{
			text2 += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		}
		if (useSpe)
		{
			text2 += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
		}
		for (int i = 0; i < length; i++)
		{
			text += text2.Substring(random.Next(0, text2.Length - 1), 1);
		}
		return text;
	}

	public static void ShowMessageWithColor(string message, Color color = default(Color))
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		string arg = ColorUtility.ToHtmlStringRGB(color);
		ConsoleController.ShowMessage(string.Format("<color=#{0}>{1}{2}</color>", arg, message, " -- Form Block Enhancement Mod"));
	}
}
