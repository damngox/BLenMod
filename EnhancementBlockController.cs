using System;
using System.Collections.Generic;
using BlockEnhancementMod.Blocks;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace BlockEnhancementMod;

internal class EnhancementBlockController : SingleInstance<EnhancementBlockController>
{
	internal PlayerMachineInfo PMI;

	public Dictionary<int, Type> dic_EnhancementBlock = new Dictionary<int, Type>
	{
		{
			44,
			typeof(BallJointScript)
		},
		{
			43,
			typeof(Balloon_EnhanceScript)
		},
		{
			11,
			typeof(CannonScript)
		},
		{
			53,
			typeof(CanonBlock_GenericEnhanceScript)
		},
		{
			39,
			typeof(CogMotoControllerHinge_GenericEnhanceScript)
		},
		{
			4,
			typeof(DecouplerScript)
		},
		{
			14,
			typeof(FlyingBlock_EnhanceScript)
		},
		{
			49,
			typeof(GripPadScript)
		},
		{
			18,
			typeof(PistonScript)
		},
		{
			26,
			typeof(Propeller_GenericEnhanceScript)
		},
		{
			55,
			typeof(Propeller_GenericEnhanceScript)
		},
		{
			25,
			typeof(Propeller_GenericEnhanceScript)
		},
		{
			34,
			typeof(Propeller_GenericEnhanceScript)
		},
		{
			52,
			typeof(PropellerScript_52)
		},
		{
			50,
			typeof(SmallwheelScript)
		},
		{
			22,
			typeof(CogMotoControllerHinge_GenericEnhanceScript)
		},
		{
			9,
			typeof(SpringScript)
		},
		{
			28,
			typeof(Steering_Hinge)
		},
		{
			13,
			typeof(SteeringBlockScript)
		},
		{
			16,
			typeof(SuspensionScript)
		},
		{
			45,
			typeof(SpringCode_GenericEnhanceScript)
		},
		{
			21,
			typeof(FlamethrowerScript)
		},
		{
			2,
			typeof(WheelScript)
		},
		{
			46,
			typeof(WheelScript)
		},
		{
			60,
			typeof(WheelScript)
		},
		{
			40,
			typeof(WheelScript)
		},
		{
			59,
			typeof(RocketScript)
		},
		{
			58,
			typeof(CameraScript)
		},
		{
			15,
			typeof(WoodenScript)
		},
		{
			1,
			typeof(WoodenScript)
		},
		{
			63,
			typeof(WoodenScript)
		},
		{
			10,
			typeof(WoodenScript)
		},
		{
			41,
			typeof(WoodenScript)
		},
		{
			56,
			typeof(WaterCannonScript)
		},
		{
			24,
			typeof(ArmorScript)
		},
		{
			29,
			typeof(ArmorRoundScript)
		},
		{
			73,
			typeof(BuildSurfaceScript)
		},
		{
			5,
			typeof(HingeScript)
		},
		{
			74,
			typeof(SqrBalloonScript)
		}
	};

	public override string Name { get; } = "Enhancement Block Controller";

	private void Awake()
	{
		Events.OnMachineLoaded += delegate(PlayerMachineInfo pmi)
		{
			PMI = pmi;
		};
		Events.OnBlockInit += AddSliders;
	}

	private void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		if (!StatMaster.levelSimulating && (int)SingleInstanceFindOnly<AddPiece>.Instance.CurrentType == 55 && Input.GetKeyDown((KeyCode)304))
		{
			SingleInstanceFindOnly<AddPiece>.Instance.SetBlockType((BlockType)52);
			SingleInstanceFindOnly<AddPiece>.Instance.clickSound.Play();
		}
	}

	public static bool HasEnhancement(BlockBehaviour block)
	{
		return ((SaveableDataHolder)block).MapperTypes.Exists((MapperType match) => match.Key == "Enhancement");
	}

	public void AddAllSliders()
	{
		foreach (BlockBehaviour item in Machine.Active().BuildingBlocks.FindAll((BlockBehaviour block) => !HasEnhancement(block)))
		{
			AddSliders(item);
		}
	}

	private void AddSliders(Block block)
	{
		BlockBehaviour internalObject = block.BuildingBlock.InternalObject;
		if (!HasEnhancement(internalObject))
		{
			AddSliders(internalObject);
		}
	}

	private void AddSliders(Transform block)
	{
		BlockBehaviour component = ((Component)block).GetComponent<BlockBehaviour>();
		if (!HasEnhancement(component))
		{
			AddSliders(component);
		}
	}

	private void AddSliders(BlockBehaviour block)
	{
		if (dic_EnhancementBlock.ContainsKey(block.BlockID))
		{
			Type type = dic_EnhancementBlock[block.BlockID];
			if ((Object)(object)((Component)block).GetComponent(type) == (Object)null)
			{
				((Component)block).gameObject.AddComponent(type);
			}
		}
	}
}
