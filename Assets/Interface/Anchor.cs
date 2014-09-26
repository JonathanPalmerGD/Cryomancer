using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour
{
	public int id;
	public float depth;
	//public Vector2 topLeft;
	//public Vector2 size;
	public TextAnchor anchorAlignment = TextAnchor.MiddleCenter;

	public enum GUIType { Label, Box };
	public GUIType gType = GUIType.Box;
	//public Vector2 padding;
	public Vector4 padding;

	public GUIContent content;

	public void Draw(Rect parent)
	{
		GUIStyle drawStyle = GUIStyle.none;
		if (gType == GUIType.Label)
			drawStyle = GUI.skin.label;
		else if(gType == GUIType.Box)
			drawStyle = GUI.skin.box;
		
		drawStyle.alignment = anchorAlignment;

		Rect myRect = new Rect(parent.xMin + padding.x, parent.yMin + padding.y, parent.width - (padding.x + padding.z), parent.height - (padding.y + padding.w));

		GUI.Label(myRect, content, drawStyle);
	}
}
