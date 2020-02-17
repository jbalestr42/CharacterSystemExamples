﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    GameObject _healthFill;

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
}
