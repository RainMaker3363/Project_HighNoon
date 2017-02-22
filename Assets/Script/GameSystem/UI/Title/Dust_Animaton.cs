using UnityEngine;
using System.Collections;

public class Dust_Animaton : MonoBehaviour {

    private bool switchingAni;

    private Animator ani;
    public GameObject Dust_Ani;

	// Use this for initialization
	void Start () {

        if(ani == null)
        {
            ani = GetComponent<Animator>();
        }

        if(Dust_Ani == null)
        {
            Dust_Ani = GameObject.Find("Dust_BG_02");
            Dust_Ani.GetComponent<Animator>().enabled = false;
            //Dust_Ani.GetComponent<Animator>().Stop();
        }
        else
        {
            Dust_Ani.GetComponent<Animator>().enabled = false;
        }
       
	}

    public void Switiching(int num)
    {
        ani.enabled = false;
        Dust_Ani.GetComponent<Animator>().enabled = true;

    }
}
