using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
            Object.Destroy(this);

        else Object.DontDestroyOnLoad(this);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
