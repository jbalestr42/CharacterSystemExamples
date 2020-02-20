using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject _manaFill = null;

    AttributeManager _attributeManager = null;

    void Start()
    {
        _attributeManager = GetComponent<AttributeManager>();
        Attribute<float> health = _attributeManager.GetAttribute<float>(AttributeType.Mana);
        health.OnValueChanged += OnManaChanged;
    }

    public void OnManaChanged(Attribute<float> p_attribute)
    {
        _manaFill.GetComponent<UnityEngine.UI.Image>().fillAmount = p_attribute.Value / p_attribute.GetValue(AttributeValueType.Max);
    }
}
