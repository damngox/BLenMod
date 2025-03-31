using System.Collections.Generic;
using Modding;
using Modding.Levels;
using UnityEngine;

namespace BlockEnhancementMod;

public class EnhancementEventsController : SingleInstance<EnhancementEventsController>
{
	private List<Entity> RestoreEntities = new List<Entity>();

	public override string Name { get; } = "Events Controller";

	public void OnGroup(LogicChain logicChain, IDictionary<string, EventProperty> propertise)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		RestoreEntities.Clear();
		Transform parent = logicChain.Entity.GameObject.transform.parent;
		foreach (Entity entity in ((Picker)propertise["Picker"]).Entities)
		{
			if (entity.InternalObject.isStatic)
			{
				entity.GameObject.transform.SetParent(logicChain.Entity.GameObject.transform);
				RestoreEntities.Add(entity);
				entity.GameObject.AddComponent<restorScript>().Parent = parent;
			}
		}
	}
}
