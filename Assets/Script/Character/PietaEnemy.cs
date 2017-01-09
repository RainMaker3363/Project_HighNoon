using UnityEngine;
using System.Collections;

public class PietaEnemy : MonoBehaviour {

    public GameObject MainCamera;

	// Use this for initialization
	void Start () {
	
        if(MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.LookAt(new Vector3(this.transform.position.x, 0, MainCamera.transform.position.z));
	}
}
