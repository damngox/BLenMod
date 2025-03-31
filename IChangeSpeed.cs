namespace BlockEnhancementMod;

public interface IChangeSpeed
{
	float Speed { get; set; }

	MSlider SpeedSlider { get; set; }

	MKey AddSpeedKey { get; set; }

	MKey ReduceSpeedKey { get; set; }

	MValue ChangeSpeedValue { get; set; }
}
