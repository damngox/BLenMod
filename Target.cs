using System.Collections.Generic;
using UnityEngine;

namespace BlockEnhancementMod;

public class Target
{
	public enum category
	{
		Basic = 1,
		Armour = 1,
		Machanical = 2,
		Locomotion = 3,
		Flight = 3,
		ModBlock = 3,
		Automation = 4,
		Primitives = 1,
		EnvironmentFoliage = 1,
		Brick = 2,
		Buildings = 2,
		Animals = 3,
		Humans = 4,
		Virtual = -1,
		Weather = -1,
		All = -1,
		Weaponry = 5,
		Point = 10
	}

	private Transform transform;

	private Collider collider;

	private BlockBehaviour block;

	private GenericEntity entity;

	private Rigidbody rigidbody;

	private FireTag fireTag;

	private Joint joint;

	private TimedRocket rocket;

	private ExplodeOnCollideBlock bomb;

	private List<int> weapons = new List<int>
	{
		20, 3, 17, 48, 11, 53, 61, 21, 62, 56,
		47, 23, 54, 59, 31, 36
	};

	private List<int> basic = new List<int> { 0, 15, 1, 63, 41 };

	private List<int> armour = new List<int> { 24, 32, 29, 10, 49, 33, 37, 30, 6 };

	private List<int> machanical = new List<int>
	{
		19, 5, 44, 22, 16, 42, 18, 4, 27, 9,
		45
	};

	private List<int> locomotion = new List<int> { 28, 13, 2, 46, 40, 60, 50, 39, 38, 51 };

	private List<int> flight = new List<int> { 14, 26, 55, 52, 25, 34, 35, 43 };

	private List<int> automation = new List<int> { 65, 66, 67, 68, 69, 70 };

	public Vector3 Position
	{
		get
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)rigidbody != (Object)null))
			{
				return Vector3.zero;
			}
			return transform.TransformPoint(rigidbody.centerOfMass);
		}
	}

	public Vector3 Velocity
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)rigidbody != (Object)null))
			{
				return Vector3.zero;
			}
			return rigidbody.velocity;
		}
	}

	public category Category { get; private set; } = category.Virtual;

	public float WarningValue { get; private set; } = -1f;

	public bool Enable
	{
		get
		{
			return IsEnabled();
		}
		private set
		{
			Enable = value;
		}
	}

	public Target()
	{
	}

	public Target(Vector3 point)
	{
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Expected O, but got Unknown
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject("Target Object");
		val.AddComponent<DestroyIfEditMode>();
		val.transform.position = point;
		transform = val.transform;
	}

	public Target(Collider collider)
	{
		this.collider = collider;
		if (Enable)
		{
			transform = ((Component)collider).transform;
			rigidbody = ((Component)(object)collider).GetComponentInAll<Rigidbody>();
			block = ((Component)(object)collider).GetComponentInAll<BlockBehaviour>();
			entity = ((Component)(object)collider).GetComponentInAll<GenericEntity>();
			fireTag = ((Component)(object)collider).GetComponentInAll<FireTag>();
			rocket = ((Component)(object)collider).GetComponentInAll<TimedRocket>();
			bomb = ((Component)(object)collider).GetComponentInAll<ExplodeOnCollideBlock>();
			joint = ((Component)(object)collider).GetComponentInAll<Joint>();
			RefreshWarningValue();
		}
	}

	public string ReturnTargetName()
	{
		return ((Object)collider).name;
	}

	public Collider ReturnCollider()
	{
		return collider;
	}

	public bool ReturnIfRocket()
	{
		return (Object)(object)rocket != (Object)null;
	}

	public BlockBehaviour ReturnBlockBehaviour()
	{
		return block;
	}

	public TimedRocket ReturnTimedrocket()
	{
		return rocket;
	}

	public FireTag ReturnFireTag()
	{
		return fireTag;
	}

	public bool HasFireTag()
	{
		return (Object)(object)fireTag != (Object)null;
	}

	public bool JointBroken()
	{
		if (ReturnIfRocket())
		{
			return false;
		}
		if ((Object)(object)joint == (Object)null)
		{
			return true;
		}
		return (Object)(object)joint.connectedBody == (Object)null;
	}

	public void RefreshWarningValue()
	{
		Category = CalculateCategory();
		WarningValue = CalculateWarningValue();
	}

	public bool IsNullTarget()
	{
		return (Object)(object)transform == (Object)null;
	}

	private bool IsEnabled()
	{
		bool result = false;
		if ((Object)(object)collider != (Object)null && (Object)(object)((Component)collider).transform != (Object)null && !collider.isTrigger && IsKinematicRigidbody(collider))
		{
			result = true;
		}
		return result;
		static bool IsKinematicRigidbody(Collider _collider)
		{
			bool result2 = false;
			Rigidbody componentInAll = ((Component)(object)_collider).GetComponentInAll<Rigidbody>();
			if ((Object)(object)componentInAll != (Object)null && !componentInAll.isKinematic)
			{
				result2 = true;
			}
			return result2;
		}
	}

	private category CalculateCategory()
	{
		category result = category.Virtual;
		if ((Object)(object)block != (Object)null)
		{
			result = GetBlockCategory(block);
		}
		else if ((Object)(object)entity != (Object)null)
		{
			result = GetEntityCategory(entity);
		}
		return result;
	}

	private float CalculateWarningValue()
	{
		float num = (float)Category;
		float num2 = (((Object)(object)fireTag != (Object)null && !fireTag.burning && !fireTag.hasBeenBurned) ? 3f : 0f);
		float num3 = 1f;
		if ((Object)(object)rocket != (Object)null)
		{
			num3 = ((!rocket.hasFired) ? 5f : 10f);
		}
		float num4 = (((Object)(object)bomb != (Object)null && !bomb.hasExploded) ? 4f : 1f);
		float num5 = (JointBroken() ? (-1f) : 1f);
		return num * num2 * num3 * num4 * num5;
	}

	private category GetBlockCategory(BlockBehaviour block)
	{
		int blockID = block.BlockID;
		if (weapons.Contains(blockID))
		{
			return category.Weaponry;
		}
		if (basic.Contains(blockID))
		{
			return category.Basic;
		}
		if (armour.Contains(blockID))
		{
			return category.Basic;
		}
		if (machanical.Contains(blockID))
		{
			return category.Machanical;
		}
		if (flight.Contains(blockID))
		{
			return category.Locomotion;
		}
		if (automation.Contains(blockID))
		{
			return category.Automation;
		}
		return category.Locomotion;
	}

	private category GetEntityCategory(GenericEntity entity)
	{
		string text = ((object)(Category)(ref entity.prefab.category)/*cast due to .constrained prefix*/).ToString().ToLower();
		if (text == category.Weaponry.ToString().ToLower())
		{
			return category.Weaponry;
		}
		if (text == category.Basic.ToString().ToLower())
		{
			return category.Basic;
		}
		if (text == category.Basic.ToString().ToLower())
		{
			return category.Basic;
		}
		if (text == category.Machanical.ToString().ToLower())
		{
			return category.Machanical;
		}
		if (text == category.Machanical.ToString().ToLower())
		{
			return category.Machanical;
		}
		if (text == category.Locomotion.ToString().ToLower())
		{
			return category.Locomotion;
		}
		if (text == category.Automation.ToString().ToLower())
		{
			return category.Automation;
		}
		if (text == category.Virtual.ToString().ToLower())
		{
			return category.Virtual;
		}
		_ = text == category.Virtual.ToString().ToLower();
		return category.Virtual;
	}
}
