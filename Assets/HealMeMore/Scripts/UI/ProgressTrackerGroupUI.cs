using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class ProgressTrackerGroupUI : MonoBehaviour, IProgressTrackerProvider<ProgressTrackerGroupUI.TrackerData>
{
    [SerializeField]
    public class TrackerData : ScriptableObject
    {
        public Color color = Color.white;
        public bool showDuration = true;
        public Sprite sprite = null;

        public TrackerData() {}

	    [MenuItem("Assets/Create/TrackerData")]
	    public static void CreateAsset()
	    {
		    ScriptableObjectUtility.CreateAsset<TrackerData>();
	    }
    }
    
    /// <summary>
    /// A prefab that implements the IProgressTracker interface
    /// </summary>
    [SerializeField]
    GameObject _progressTrackerPrefab = null;
    
    /// <summary>
    /// Transform set as parent for the instantiated progress tracker
    /// </summary>
    [SerializeField]
    Transform _progressTrackerGroup = null;

    public GameObject CreateTracker(TrackerData p_trackerData)
    {
        GameObject progressTrackerGameObject = Instantiate(_progressTrackerPrefab);
        IProgressTracker progressTracker = progressTrackerGameObject.GetComponent<IProgressTracker>();
        Assert.IsNotNull(progressTracker, "There is no IProgressTracker implementation on this gameObject.");
        
        progressTrackerGameObject.GetComponent<ProgressIconUI>().Init(p_trackerData.sprite, p_trackerData.color, p_trackerData.showDuration);
        progressTrackerGameObject.transform.SetParent(_progressTrackerGroup);

        return progressTrackerGameObject;
    }
}
