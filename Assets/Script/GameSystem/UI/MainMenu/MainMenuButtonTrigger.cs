using UnityEngine;
using System.Collections;

public class MainMenuButtonTrigger : MonoBehaviour {

    private Animator ani;

    // Use this for initialization
    void Start()
    {
        if (ani == null)
        {
            ani = GetComponent<Animator>();
            ani.enabled = false;
        }
        else
        {
            ani.enabled = false;
        }

        MainMenuManager.MainMenuBtnDownOn = true;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}


    void OnEnable()
    {
        if (ani == null)
        {
            ani = GetComponent<Animator>();
            ani.enabled = true;
        }
        else
        {
            ani.enabled = true;
        }

        MainMenuManager.MainMenuBtnDownOn = false;

    }

    public void ChangeMainBtnActive(int Active)
    {
        if (Active >= 1)
        {
            MainMenuManager.MainMenuBtnDownOn = true;
        }
        else if (Active <= 0)
        {
            MainMenuManager.MainMenuBtnDownOn = false;
        }


    }

}
