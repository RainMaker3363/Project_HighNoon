using UnityEngine;
using System.Collections;

public class Single_VersusAction : MonoBehaviour {

    public GameObject Single_VersusAction_Object;
    public GameObject Single_RevolverAction;
    public GameObject InGame_UI_Object;

	// Use this for initialization
	void Start () {

        if (Single_VersusAction_Object == null)
        {
            Single_VersusAction_Object = GameObject.Find("Single_VersusAction");
            Single_VersusAction_Object.SetActive(true);
        }
        else
        {
            Single_VersusAction_Object.SetActive(true);
        }

        

        if (Single_RevolverAction == null)
        {
            Single_RevolverAction = GameObject.Find("Single_RevolverAction");
            Single_RevolverAction.SetActive(false);
        }
        else
        {
            Single_RevolverAction.SetActive(false);
        }

        if(InGame_UI_Object == null)
        {
            InGame_UI_Object = GameObject.Find("Player_UI");
            InGame_UI_Object.SetActive(false);
        }
        else
        {
            InGame_UI_Object.SetActive(false);
        }



        GameManager.DeadEyeVersusAction = false;
	}

    void OnEnable()
    {
        if (Single_VersusAction_Object == null)
        {
            Single_VersusAction_Object = GameObject.Find("Single_VersusAction");
            Single_VersusAction_Object.SetActive(true);
        }
        else
        {
            Single_VersusAction_Object.SetActive(true);
        }



        if (Single_RevolverAction == null)
        {
            Single_RevolverAction = GameObject.Find("Single_RevolverAction");
            Single_RevolverAction.SetActive(false);
        }
        else
        {
            Single_RevolverAction.SetActive(false);
        }

        if (InGame_UI_Object == null)
        {
            InGame_UI_Object = GameObject.Find("Player_UI");
            InGame_UI_Object.SetActive(false);
        }
        else
        {
            InGame_UI_Object.SetActive(false);
        }

        GameManager.DeadEyeVersusAction = false;
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
            InGame_UI_Object.SetActive(false);
        }
        else
        {
            GameManager.DeadEyeVersusAction = true;
            Single_RevolverAction.SetActive(true);
            Single_VersusAction_Object.SetActive(false);
            InGame_UI_Object.SetActive(false);
        }
    }
}
