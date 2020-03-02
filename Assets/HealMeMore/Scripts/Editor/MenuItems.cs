using UnityEditor;

public class MenuItems
{
	[MenuItem("Assets/Create/DisplayData")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<SkillIcon.DisplayData>();
	}
}