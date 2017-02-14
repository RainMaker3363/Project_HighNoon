using UnityEngine;
using System.Collections;

public class GameModeButtonTrigger : MonoBehaviour {

    //private Animator ani;

    // Use this for initialization
    void Start()
    {
        //if (ani == null)
        //{
        //    ani = GetComponent<Animator>();
        //    ani.enabled = false;
        //}
        //else
        //{
        //    ani.enabled = false;
        //}
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void OnEnable()
    {
        //if (ani == null)
        //{
        //    ani = GetComponent<Animator>();
        //    ani.enabled = true;
        //}
        //else
        //{
        //    ani.enabled = true;
        //}

        MainMenuManager.MainModeBtnDownOn = false;
    }

    public void ChangeMainBtnActive(int Active)
    {
        if (Active >= 1)
        {
            MainMenuManager.MainModeBtnDownOn = true;
        }
        else if (Active <= 0)
        {
            MainMenuManager.MainModeBtnDownOn = false;
        }


    }
}
