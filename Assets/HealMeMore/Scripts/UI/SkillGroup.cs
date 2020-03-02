using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Create a skill and add it to the given group
/// </summary>
public class SkillGroup : MonoBehaviour, ISkillGroup<SkillGroup.DisplayData>
{
    [SerializeField]
    public class DisplayData : ScriptableObject
    {
        public Color color = Color.white;
        public bool showDuration = true;
        public Sprite sprite = null;

        public DisplayData() {}

	    [MenuItem("Assets/Create/DisplayData")]
	    public static void CreateAsset()
	    {
		    ScriptableObjectUtility.CreateAsset<DisplayData>();
	    }
    }
    
    /// <summary>
    /// A prefab that implements the IProgressTracker interface and ASkillController
    /// </summary>
    [SerializeField]
    GameObject _skillPrefab = null;
    
    /// <summary>
    /// Transform set as parent for the instantiated skill
    /// </summary>
    [SerializeField]
    Transform _group = null;

    void Start()
    {
        Assert.IsNotNull(_skillPrefab, "A gameObject must be provided");
        Assert.IsNotNull(_skillPrefab.GetComponent<IProgressTracker>(), "There is no IProgressTracker implementation on this gameObject.");
        Assert.IsNotNull(_skillPrefab.GetComponent<ASkillController>(), "There is no ASkillController implementation on this gameObject.");
        Assert.IsNotNull(_skillPrefab.GetComponent<SkillIcon>(), "There is no SkillIcon implementation on this gameObject.");
        Assert.IsNotNull(_group, "A gameObject must be provided");
    }

    public GameObject CreateSkill(DisplayData p_displayData)
    {
        Assert.IsNotNull(p_displayData, "Data must be provided.");
        
        GameObject skill = Instantiate(_skillPrefab);
        skill.GetComponent<SkillIcon>().Init(p_displayData.sprite, p_displayData.color, p_displayData.showDuration);
        skill.transform.SetParent(_group);

        return skill;
    }
}
