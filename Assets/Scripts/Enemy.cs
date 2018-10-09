using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float colliderSizeScale = 1f;
    [SerializeField] Transform garbageBin;
    [SerializeField] GameObject deathPrefab;

    BoxCollider BoxCollider;

	// Use this for initialization
	void Start () {
        AddNonTriggerBoxCollider();
	}

    private void AddNonTriggerBoxCollider()
    {
        BoxCollider = gameObject.AddComponent<BoxCollider>();
        BoxCollider.isTrigger = false;
        BoxCollider.size *= colliderSizeScale;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        GameObject deathInstance = Instantiate(deathPrefab, gameObject.transform.position, Quaternion.identity);
        deathInstance.transform.localScale = Vector3.ClampMagnitude(gameObject.transform.localScale, 5);
        deathInstance.SetActive(true);
        deathInstance.transform.parent = garbageBin;
        UnityEngine.Object.Destroy(gameObject);
    }
}
