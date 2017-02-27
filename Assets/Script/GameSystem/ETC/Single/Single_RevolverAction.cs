using UnityEngine;
using System.Collections;

public class Single_RevolverAction : MonoBehaviour {

    public GameObject Single_RevolverTouch;
    public GameObject Single_RevolverLetterBox;
    public GameObject Single_RevolverAction_BG;
    //public GameObject Single_RevolverAction_Object;
    private Animator ani;

    public SoundManager SDManager;
    public AudioClip CommandStart_Sound;
    

	// Use this for initialization
	void Start () {

        if(ani == null)
        {
            ani = GetComponent<Animator>();
            ani.enabled = true;
        }
        else
        {
            ani.enabled = true;
        }

        if (Single_RevolverLetterBox == null)
        {
            Single_RevolverLetterBox = GameObject.Find("Letterbox");
            Single_RevolverLetterBox.GetComponent<Animator>().enabled = true;
        }
        else
        {
            Single_RevolverLetterBox.GetComponent<Animator>().enabled = true;
        }

        if (Single_RevolverAction_BG == null)
        {
            Single_RevolverAction_BG = GameObject.Find("RevolverAction_BG");
            Single_RevolverAction_BG.SetActive(true);
        }
        else
        {
            Single_RevolverAction_BG.SetActive(true);
        }

        SDManager.PlaySfx(CommandStart_Sound);

        GameManager.DeadEyeRevolverAction = true;
        Single_RevolverTouch.SetActive(false);
	}

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

        if (Single_RevolverLetterBox == null)
        {
            Single_RevolverLetterBox = GameObject.Find("Letterbox");
            Single_RevolverLetterBox.GetComponent<Animator>().enabled = true;
        }
        else
        {
            Single_RevolverLetterBox.GetComponent<Animator>().enabled = true;
        }

        if (Single_RevolverAction_BG == null)
        {
            Single_RevolverAction_BG = GameObject.Find("RevolverAction_BG");
            Single_RevolverAction_BG.SetActive(true);
        }
        else
        {
            Single_RevolverAction_BG.SetActive(true);
        }

        SDManager.PlaySfx(CommandStart_Sound);

        GameManager.DeadEyeRevolverAction = true;
        Single_RevolverTouch.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Single_RevolverActionOn(int switchOn)
    {
        if (switchOn >= 1)
        {
            GameManager.DeadEyeRevolverAction = false;
            Single_RevolverTouch.SetActive(true);
            Single_RevolverAction_BG.SetActive(false);
            ani.enabled = false;
            //ani.Stop();
            Single_RevolverLetterBox.GetComponent<Animator>().enabled = false;
            //Single_RevolverLetterBox.GetComponent<Animator>().Stop();

            //Single_RevolverAction_Object.gameObject.SetActive(false);
        }
        else
        {
            GameManager.DeadEyeRevolverAction = false;
            Single_RevolverTouch.SetActive(true);
            Single_RevolverAction_BG.SetActive(false);
            ani.enabled = false;
            //ani.Stop();
            Single_RevolverLetterBox.GetComponent<Animator>().enabled = false;
            //Single_RevolverLetterBox.GetComponent<Animator>().Stop();
            //Single_RevolverAction_Object.gameObject.SetActive(false);
        }
    }
}
