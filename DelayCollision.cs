using UnityEngine;

namespace BlockEnhancementMod;

internal class DelayCollision : MonoBehaviour
{
	public float Delay { get; set; } = 0.2f;

	private void OnEnable()
	{
		((MonoBehaviour)this).Invoke("delayCollision", Delay);
	}

	private void delayCollision()
	{
		((Component)this).GetComponent<Collider>().enabled = true;
		((Component)this).GetComponent<Rigidbody>().detectCollisions = true;
	}
}
