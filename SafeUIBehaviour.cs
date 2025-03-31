using Modding;
using UnityEngine;

namespace BlockEnhancementMod;

public abstract class SafeUIBehaviour : MonoBehaviour
{
	public Rect windowRect = new Rect(0f, 0f, 192f, 128f);

	private GameObject background;

	public int windowID { get; protected set; } = ModUtility.GetWindowId();

	public string windowName { get; set; } = "";

	public abstract bool ShouldShowGUI { get; set; }

	private void Awake()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		background = new GameObject("UIBackGround");
		background.transform.SetParent(((Component)this).gameObject.transform);
		background.layer = 13;
		background.AddComponent<BoxCollider>();
		SafeAwake();
	}

	public virtual void OnGUI()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)GameObject.Find("HUD Cam") == (Object)null)
		{
			return;
		}
		if (ShouldShowGUI)
		{
			if (!background.activeSelf)
			{
				background.SetActive(true);
			}
			Camera component = GameObject.Find("HUD Cam").GetComponent<Camera>();
			Ray val = component.ScreenPointToRay(new Vector3(((Rect)(ref windowRect)).xMin, (float)component.pixelHeight - ((Rect)(ref windowRect)).yMin, 0f));
			Vector3 origin = ((Ray)(ref val)).origin;
			val = component.ScreenPointToRay(new Vector3(((Rect)(ref windowRect)).xMax, (float)component.pixelHeight - ((Rect)(ref windowRect)).yMax, 0f));
			Vector3 origin2 = ((Ray)(ref val)).origin;
			Vector3 position = (origin + origin2) * 0.5f;
			position.z += 0.3f;
			background.transform.position = position;
			Vector3 val2 = origin2 - origin;
			val2.z = 0.1f;
			val2.x = Mathf.Abs(val2.x);
			val2.y = Mathf.Abs(val2.y);
			background.transform.localScale = val2;
			windowRect = GUILayout.Window(windowID, windowRect, new WindowFunction(WindowContent), windowName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		else if (background.activeSelf)
		{
			background.SetActive(false);
		}
	}

	protected abstract void WindowContent(int windowID);

	public virtual void SafeAwake()
	{
	}
}
