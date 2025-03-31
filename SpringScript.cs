using UnityEngine;

namespace BlockEnhancementMod.Blocks;

internal class SpringScript : EnhancementBlock
{
	private MSlider DragSlider;

	private Rigidbody A;

	private Rigidbody B;

	public override void SafeAwake()
	{
		DragSlider = AddSlider(SingleInstance<LanguageManager>.Instance.CurrentLanguage.Drag, "Drag", 2f, 0f, 3f);
	}

	public override void DisplayInMapper(bool value)
	{
		((MapperType)DragSlider).DisplayInMapper = value;
	}

	public override void OnSimulateStartAlways()
	{
		if (base.EnhancementEnabled)
		{
			A = GameObject.Find("A").GetComponent<Rigidbody>();
			B = GameObject.Find("B").GetComponent<Rigidbody>();
			Rigidbody a = A;
			float drag = (B.drag = DragSlider.Value);
			a.drag = drag;
		}
	}
}
