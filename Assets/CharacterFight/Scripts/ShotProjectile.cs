﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotProjectile : ASkill {

	public GameObject _bullet;

	void Start() {
        var requirement = new List<IRequirement>();
		requirement.Add(new InputReq("space"));
		base.Init(1.0f, 5.0f, requirement, GetComponent<Character>()._skillGroup.Add(Color.cyan, true));
    }

    public override void Cast(GameObject p_owner)
    {
	}
}
