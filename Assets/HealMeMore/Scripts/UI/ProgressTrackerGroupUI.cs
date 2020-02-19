using UnityEngine;
using UnityEngine.Assertions;

public class ProgressTrackerGroupUI : MonoBehaviour, IProgressTrackerProvider
{
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

    public IProgressTracker CreateTracker()
    {
        GameObject progressTrackerGameObject = Instantiate(_progressTrackerPrefab);
        IProgressTracker progressTracker = progressTrackerGameObject.GetComponent<IProgressTracker>();
        Assert.IsNotNull(progressTracker, "There is no IProgressTracker implementation on this gameObject.");
        
        progressTrackerGameObject.GetComponent<ProgressIconUI>().Init(Color.cyan, true);
        progressTrackerGameObject.transform.SetParent(_progressTrackerGroup);

        return progressTracker;
    }
}
