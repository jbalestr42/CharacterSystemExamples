using UnityEngine;
using UnityEngine.Assertions;

public class CharacterUI : MonoBehaviour, IProgressTrackerProvider
{
    [SerializeField]
    GameObject _progressTrackerPrefab;
    
    [SerializeField]
    GameObject _healthFill = null;

    /// <summary>
    ///  Parent for the skill progress icon
    /// </summary>
    [SerializeField]
    GameObject _skillProgressGroup = null;

    AttributeManager _attributeManager = null;

    void Start()
    {
        _attributeManager = GetComponent<AttributeManager>();
        Attribute<float> health = _attributeManager.GetAttribute<float>(AttributeType.Health);
        health.OnValueChanged += OnHealthChanged;
    }

    public void OnHealthChanged(Attribute<float> p_attribute)
    {
        _healthFill.GetComponent<UnityEngine.UI.Image>().fillAmount = p_attribute.Value / p_attribute.GetValue(AttributeValueType.Max);
    }
    
    public IProgressTracker CreateTracker()
    {
        GameObject progressTrackerGameObject = Instantiate(_progressTrackerPrefab);
        IProgressTracker progressTracker = progressTrackerGameObject.GetComponent<IProgressTracker>();
        Assert.IsNotNull(progressTracker, "There is no IProgressTracker implementation on this gameObject.");
        
        progressTrackerGameObject.GetComponent<ProgressIconUI>().Init(Color.cyan, true);
        progressTrackerGameObject.transform.SetParent(_skillProgressGroup.transform);

        return progressTracker;
    }
}
