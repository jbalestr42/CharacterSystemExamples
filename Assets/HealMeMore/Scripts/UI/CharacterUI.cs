using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    GameObject _healthFill = null;
    
    [SerializeField]
    GameObject _healthText = null;

    AttributeManager _attributeManager = null;
    Attribute<float> _health = null;
    Attribute<float> _healthMax = null;

    void Start()
    {
        _attributeManager = GetComponent<AttributeManager>();
        _health = _attributeManager.GetAttribute<float>(AttributeType.Health);
        _healthMax = _attributeManager.GetAttribute<float>(AttributeType.HealthMax);
        
        _health.AddOnValueChangedDelegate(OnHealthChanged);
        _healthMax.AddOnValueChangedDelegate(OnHealthChanged);
    }

    public void OnHealthChanged(Attribute<float> p_attribute)
    {
        _healthFill.GetComponent<UnityEngine.UI.Image>().fillAmount = _health.Value / _healthMax.Value;
        _healthText.GetComponent<UnityEngine.UI.Text>().text = _health.Value + " / " + _healthMax.Value;
    }
}
