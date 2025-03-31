using System.Collections;
using Modding;
using UnityEngine;

namespace BlockEnhancementMod;

public class restorScript : MonoBehaviour
{
	public Transform Parent;

	private void Awake()
	{
		Events.OnSimulationToggle += delegate(bool value)
		{
			if (!value)
			{
				((MonoBehaviour)this).StartCoroutine(Restore());
			}
		};
	}

	private IEnumerator Restore()
	{
		yield return 0;
		((Component)this).transform.SetParent(Parent);
		Debug.Log((object)"restore");
		Object.Destroy((Object)(object)((Component)this).GetComponent<restorScript>());
	}
}
