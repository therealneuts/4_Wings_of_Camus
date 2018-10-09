using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int health = 3;
    [SerializeField] float colliderSizeScale = 1f;
    [SerializeField] int scoreValue = 10;

    [Header("FX")]
    [SerializeField] Transform garbageBin;
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;

    ScoreBoard scoreBoard;

    BoxCollider BoxCollider;

    int healthRemaining;

	// Use this for initialization
	void Start () {
        scoreBoard = FindObjectOfType<ScoreBoard>();

        AddNonTriggerBoxCollider();

        healthRemaining = health;
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

        GameObject hitInstance = Instantiate(hitFX, gameObject.transform.position, Quaternion.Inverse(other.transform.rotation));
        hitInstance.transform.parent = garbageBin;

        if (--healthRemaining == 0)
        {
            //Instantiate and clean up death FX
            GameObject deathInstance = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
            deathInstance.transform.localScale = Vector3.ClampMagnitude(gameObject.transform.localScale, 5);
            deathInstance.SetActive(true);
            deathInstance.transform.parent = garbageBin;

            //Increment score
            scoreBoard.IncrementScore(scoreValue);

            //Destroy object
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
