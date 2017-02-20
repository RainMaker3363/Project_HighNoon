using UnityEngine;
using System.Collections;

public class Single_RevolverAction : MonoBehaviour {

    public GameObject Single_RevolverTouch;
    public GameObject Single_RevolverAction_Object;

	// Use this for initialization
	void Start () {

        GameManager.DeadEyeRevolverAction = false;
        Single_RevolverTouch.SetActive(false);
	}

    void OnEnable()
    {
        GameManager.DeadEyeRevolverAction = false;
        Single_RevolverTouch.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Single_RevolverActionOn(int switchOn)
    {
        if (switchOn >= 1)
        {
            GameManager.DeadEyeRevolverAction = true;
            Single_RevolverTouch.SetActive(true);
            Single_RevolverAction_Object.gameObject.SetActive(false);
        }
        else
        {
            GameManager.DeadEyeRevolverAction = false;
            Single_RevolverTouch.SetActive(false);
            Single_RevolverAction_Object.gameObject.SetActive(false);
        }
    }
}
