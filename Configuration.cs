using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace BlockEnhancementMod;

public class Configuration
{
	public class Property<T>
	{
		public string Key = "";

		public T Value;

		public Property(string key, T value)
		{
			Key = key;
			Value = value;
		}

		public override string ToString()
		{
			return Key + " - " + Value.ToString();
		}
	}

	private static Dictionary<Type, Func<XDataHolder, string, object>> typeSpecialAction = new Dictionary<Type, Func<XDataHolder, string, object>>
	{
		{
			typeof(int),
			(XDataHolder xDataHolder, string key) => xDataHolder.ReadInt(key)
		},
		{
			typeof(bool),
			(XDataHolder xDataHolder, string key) => xDataHolder.ReadBool(key)
		},
		{
			typeof(float),
			(XDataHolder xDataHolder, string key) => xDataHolder.ReadFloat(key)
		},
		{
			typeof(string),
			(XDataHolder xDataHolder, string key) => xDataHolder.ReadString(key)
		},
		{
			typeof(Color),
			(XDataHolder xDataHolder, string key) => xDataHolder.ReadColor(key)
		},
		{
			typeof(Vector3),
			(XDataHolder xDataHolder, string key) => xDataHolder.ReadVector3(key)
		}
	};

	internal static ArrayList Properties { get; private set; } = new ArrayList
	{
		new Property<bool>("Enhance More", value: false),
		new Property<bool>("ShowUI", value: true),
		new Property<bool>("Friction", value: false),
		new Property<bool>("Display Warning", value: true),
		new Property<bool>("Mark Target", value: true),
		new Property<bool>("Display Rocket Count", value: true),
		new Property<float>("GuideControl P Factor", 1.25f),
		new Property<float>("GuideControl I Factor", 10f),
		new Property<float>("GuideControl D Factor", 0f),
		new Property<float>("Rocket Smoke Emission Constant", 80f),
		new Property<float>("Rocket Smoke Lifetime", 1f),
		new Property<float>("Rocket Smoke Size", 3.5f),
		new Property<Color>("Rocket Smoke Start Color", new Color(0.1f, 0.14f, 0.15f, 1f)),
		new Property<Color>("Rocket Smoke End Color", new Color(0.1f, 0.14f, 0.15f, 1f)),
		new Property<float>("Rocket Smoke Start Color Time", 0.09f),
		new Property<float>("Rocket Smoke End Color Time", 1f),
		new Property<float>("Rocket Smoke Start Alpha", 1f),
		new Property<float>("Rocket Smoke End Alpha", 1f),
		new Property<float>("Rocket Smoke Start Alpha Time", 0.076f),
		new Property<float>("Rocket Smoke End Alpha Time", 0.26f),
		new Property<int>("Radar Frequency", 5),
		new Property<bool>("BuildSurface", value: false)
	};

	public T GetValue<T>(string key)
	{
		T result = default(T);
		foreach (object property2 in Properties)
		{
			if (property2 is Property<T>)
			{
				Property<T> property = property2 as Property<T>;
				if (property.Key == key)
				{
					result = property.Value;
					return result;
				}
			}
		}
		return result;
	}

	public void SetValue<T>(string key, T value)
	{
		bool flag = false;
		foreach (object property2 in Properties)
		{
			if (property2 is Property<T>)
			{
				Property<T> property = property2 as Property<T>;
				if (property.Key == key)
				{
					property.Value = value;
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			Properties.Add(new Property<T>(key, value));
		}
		Configuration.GetData().Write(key, (object)value);
	}

	~Configuration()
	{
		Configuration.Save();
	}

	public static Configuration FormatXDataToConfig(Configuration config = null)
	{
		XDataHolder xDataHolder = Configuration.GetData();
		bool reWrite = true;
		bool needWrite = false;
		if (config == null)
		{
			reWrite = false;
			needWrite = true;
			config = new Configuration();
		}
		for (int i = 0; i < Properties.Count; i++)
		{
			object obj = Properties[i];
			if (obj is Property<int>)
			{
				obj = getValue<int>(obj as Property<int>);
			}
			else if (obj is Property<bool>)
			{
				obj = getValue<bool>(obj as Property<bool>);
			}
			else if (obj is Property<float>)
			{
				obj = getValue<float>(obj as Property<float>);
			}
			else if (obj is Property<string>)
			{
				obj = getValue<string>(obj as Property<string>);
			}
			else if (obj is Property<Vector3>)
			{
				obj = getValue<Vector3>(obj as Property<Vector3>);
			}
			else if (obj is Property<Color>)
			{
				obj = getValue<Color>(obj as Property<Color>);
			}
			Properties[i] = obj;
		}
		if (needWrite)
		{
			Configuration.Save();
		}
		return config;
		Property<T> getValue<T>(Property<T> propertise)
		{
			string key = propertise.Key;
			T val = propertise.Value;
			if (xDataHolder.HasKey(key) && !reWrite)
			{
				val = (T)Convert.ChangeType(typeSpecialAction[typeof(T)](xDataHolder, key), typeof(T));
			}
			else
			{
				xDataHolder.Write(key, (object)val);
				needWrite = true;
			}
			return new Property<T>(key, val);
		}
	}
}
