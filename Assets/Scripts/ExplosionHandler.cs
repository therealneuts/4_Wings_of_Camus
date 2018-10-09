using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour {

    [SerializeField] GameObject deathFX;

    private void Start()
    {

    }

    void ExplodeShip()
    {
        deathFX.SetActive(true);
    }
}
