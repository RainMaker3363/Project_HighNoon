using UnityEngine;
using System.Collections;

public class Single_VersusAction : MonoBehaviour {

    public GameObject Single_VersusAction_Object;
    public GameObject Single_RevolverAction;

	// Use this for initialization
	void Start () {

        GameManager.DeadEyeVersusAction = false;
        Single_RevolverAction.SetActive(false);
	}

    void OnEnable()
    {
        GameManager.DeadEyeVersusAction = false;
        Single_RevolverAction.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Single_VersusActionOn(int switchOn)
    {
        if(switchOn >= 1)
        {
            GameManager.DeadEyeVersusAction = true;
            Single_RevolverAction.SetActive(true);
            Single_VersusAction_Object.SetActive(false);
        }
        else
        {
            GameManager.DeadEyeVersusAction = false;
            Single_RevolverAction.SetActive(true);
            Single_VersusAction_Object.SetActive(false);
        }
    }
}
