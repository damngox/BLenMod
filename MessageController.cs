using System;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace BlockEnhancementMod;

public class MessageController : SingleInstance<MessageController>
{
	public override string Name { get; } = "Message Controller";

	public MessageController()
	{
		Messages.rocketFiredMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[1] { (DataType)11 });
		Messages.rocketTargetBlockBehaviourMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[2]
		{
			(DataType)11,
			(DataType)11
		});
		Messages.rocketTargetEntityMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[2]
		{
			(DataType)9,
			(DataType)11
		});
		Messages.rocketTargetNullMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[1] { (DataType)11 });
		Messages.rocketRayToHostMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[3]
		{
			(DataType)8,
			(DataType)8,
			(DataType)11
		});
		Messages.rocketHighExploPosition = ModNetworking.CreateMessageType((DataType[])(object)new DataType[2]
		{
			(DataType)8,
			(DataType)4
		});
		Messages.rocketLockOnMeMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[2]
		{
			(DataType)11,
			(DataType)2
		});
		Messages.rocketLostTargetMsg = ModNetworking.CreateMessageType((DataType[])(object)new DataType[1] { (DataType)11 });
		CallbacksWrapper callbacks = ModNetworking.Callbacks;
		MessageType rocketHighExploPosition = Messages.rocketHighExploPosition;
		callbacks[rocketHighExploPosition] += (Action<Message>)delegate(Message msg)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			if (StatMaster.isClient)
			{
				Vector3 position = (Vector3)msg.GetData(0);
				float num = (float)msg.GetData(1);
				int key = 4;
				int num2 = 5001;
				float num3 = 7f;
				float num4 = 3600f;
				float num5 = 100000f;
				float upPower = 0.25f;
				try
				{
					GameObject obj = Object.Instantiate<GameObject>(((Component)PrefabMaster.LevelPrefabs[key].GetValue(num2)).gameObject);
					obj.transform.position = position;
					ExplodeOnCollide component = obj.GetComponent<ExplodeOnCollide>();
					obj.transform.localScale = Vector3.one * num;
					component.radius = num3 * num;
					component.power = num4 * num;
					component.torquePower = num5 * num;
					component.upPower = upPower;
					component.Explodey();
				}
				catch
				{
				}
			}
		};
		callbacks = ModNetworking.Callbacks;
		rocketHighExploPosition = Messages.rocketFiredMsg;
		callbacks[rocketHighExploPosition] += (Action<Message>)delegate(Message msg)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			Block val = (Block)msg.GetData(0);
			TimedRocket component2 = val.GameObject.GetComponent<TimedRocket>();
			SingleInstance<RocketsController>.Instance.UpdateRocketFiredStatus(component2);
			val.GameObject.GetComponentInChildren<RadarScript>();
			_ = 1;
			/*Error near IL_002e: Invalid metadata token*/;
		};
		callbacks = ModNetworking.Callbacks;
		rocketHighExploPosition = Messages.rocketTargetNullMsg;
		callbacks[rocketHighExploPosition] += (Action<Message>)delegate(Message msg)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			Block val2 = (Block)msg.GetData(0);
			if (!(val2 == (Block)null))
			{
				RadarScript componentInChildren = val2.GameObject.GetComponentInChildren<RadarScript>();
				if (!((Object)(object)componentInChildren == (Object)null))
				{
					_ = 1;
					/*Error near IL_002f: Invalid metadata token*/;
				}
			}
		};
		callbacks = ModNetworking.Callbacks;
		rocketHighExploPosition = Messages.rocketRayToHostMsg;
		callbacks[rocketHighExploPosition] += (Action<Message>)delegate(Message msg)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			((Block)msg.GetData(2)).GameObject.GetComponentInChildren<RadarScript>();
			new Ray((Vector3)msg.GetData(0), (Vector3)msg.GetData(1));
			/*Error near IL_0034: Invalid metadata token*/;
		};
		callbacks = ModNetworking.Callbacks;
		rocketHighExploPosition = Messages.rocketLockOnMeMsg;
		callbacks[rocketHighExploPosition] += (Action<Message>)delegate(Message msg)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			Block val3 = (Block)msg.GetData(0);
			int targetMachineID = (int)msg.GetData(1);
			SingleInstance<RocketsController>.Instance.UpdateRocketTarget(val3.InternalObject, targetMachineID);
		};
		callbacks = ModNetworking.Callbacks;
		rocketHighExploPosition = Messages.rocketLostTargetMsg;
		callbacks[rocketHighExploPosition] += (Action<Message>)delegate(Message msg)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			Block val4 = (Block)msg.GetData(0);
			if (!(val4 == (Block)null))
			{
				SingleInstance<RocketsController>.Instance.RemoveRocketTarget(val4.InternalObject);
			}
		};
	}
}
