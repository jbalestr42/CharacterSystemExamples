﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : AAttributeModifier<ResourceAttributeParam, ResourceAttribute> {

    Attribute<float> _regen;
    Attribute<float> _max;
    float _regenRate = 1f;
    float _timer = 0f;

    public override void OnStart(GameObject p_owner) {
        _regen = p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.regenAttributeType);
        _max = p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.maxAttributeType);
    }

    public override void ApplyModifierCast(GameObject p_owner, ResourceAttribute p_attribute) {
        _timer += Time.deltaTime;
        if (_timer >= _regenRate) {
            _timer -= _regenRate;
            Param.value = _regen.Value;
            p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.attributeType).SetValue(AttributeValueType.Add, _regen.Value);
            p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.attributeType).SetValue(AttributeValueType.Max, _max.Value);
        }
    }
}