using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Single_RevolverTouch : MonoBehaviour {

    public GameObject Single_RevolverAction_Object;
    public GameObject Single_RevolverTouch_BG_object;
    public GameObject Single_RevolverLetterBox_Object;
    public GameObject Single_RevolverBullets_Object;
    public GameObject InGame_UI_Object;

    public Text AnnounceText;

    public static int BulletChecker;

    private IEnumerator DeadEyeEndCoroutine;
    private List<int> BulletIndex = new List<int>();
    public Single_RevolverBullet[] Bullets;

	// Use this for initialization
	void Start () 
    {
        
        BulletIndex.Clear();
        
        BulletChecker = 1;

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].MyNumber = 0;
            Bullets[i].MyNumber = GenerateNumber(1, 7);
            Bullets[i].gameObject.SetActive(true);
        }

        if (Single_RevolverAction_Object == null)
        {
            Single_RevolverAction_Object = GameObject.Find("Single_RevolverAction_Object");
            Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }
        else
        {
            Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }

        if(Single_RevolverLetterBox_Object == null)
        {
            Single_RevolverLetterBox_Object = GameObject.Find("LetterBox");
            Single_RevolverLetterBox_Object.SetActive(true);
        }
        else
        {
            Single_RevolverLetterBox_Object.SetActive(true);
        }

        if(Single_RevolverTouch_BG_object == null)
        {
            Single_RevolverTouch_BG_object = GameObject.Find("DeadEye_BG");
            Single_RevolverTouch_BG_object.SetActive(true);
        }
        else
        {
            Single_RevolverTouch_BG_object.SetActive(true);
        }

        if (Single_RevolverBullets_Object == null)
        {
            Single_RevolverBullets_Object = GameObject.Find("Revolver_BG");
            Single_RevolverBullets_Object.SetActive(true);
        }
        else
        {
            Single_RevolverBullets_Object.SetActive(true);
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

        DeadEyeEndCoroutine = null;
        DeadEyeEndCoroutine = LetterBoxProtocol(true);

        AnnounceText.text = "순서대로 터치 하세요";

	}

    void OnEnable()
    {
        BulletIndex.Clear();

        BulletChecker = 1;

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].MyNumber = 0;
            Bullets[i].MyNumber = GenerateNumber(1, 7);
            Bullets[i].gameObject.SetActive(true);
        }

        if (Single_RevolverAction_Object == null)
        {
            Single_RevolverAction_Object = GameObject.Find("Single_RevolverAction_Object");
            Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }
        else
        {
            Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }

        if (Single_RevolverLetterBox_Object == null)
        {
            Single_RevolverLetterBox_Object = GameObject.Find("LetterBox");
            Single_RevolverLetterBox_Object.SetActive(true);
        }
        else
        {
            Single_RevolverLetterBox_Object.SetActive(true);
        }

        if (Single_RevolverTouch_BG_object == null)
        {
            Single_RevolverTouch_BG_object = GameObject.Find("DeadEye_BG");
            Single_RevolverTouch_BG_object.SetActive(true);
        }
        else
        {
            Single_RevolverTouch_BG_object.SetActive(true);
        }

        if (Single_RevolverBullets_Object == null)
        {
            Single_RevolverBullets_Object = GameObject.Find("Revolver_BG");
            Single_RevolverBullets_Object.SetActive(true);
        }
        else
        {
            Single_RevolverBullets_Object.SetActive(true);
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

        DeadEyeEndCoroutine = null;
        DeadEyeEndCoroutine = LetterBoxProtocol(true);

        AnnounceText.text = "순서대로 터치 하세요";
    }
	
	// Update is called once per frame
	void Update () {
        if(BulletChecker > Bullets.Length)
        {
            
            GameManager.DeadEyeVersusAction = false;
            GameManager.DeadEyeRevolverAction = false;

            Single_RevolverTouch_BG_object.SetActive(false);
            Single_RevolverAction_Object.SetActive(false);
            Single_RevolverBullets_Object.SetActive(false);

            AnnounceText.text = " ";

            DeadEyeEndCoroutine = null;
            DeadEyeEndCoroutine = LetterBoxProtocol(true);

            StopCoroutine(DeadEyeEndCoroutine);
            StartCoroutine(DeadEyeEndCoroutine);
        }

        //print("BulletChecker : " + BulletChecker);
	}

    IEnumerator LetterBoxProtocol(bool IsOn = true)
    {
        GameManager.DeadEyeActiveOn = false;

        yield return new WaitForSeconds(1.0f);

        
        Single_RevolverLetterBox_Object.SetActive(false);
        InGame_UI_Object.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public int GenerateNumber(int min, int max)
    {
        int num = Random.Range(min, max);

        while (BulletIndex.Contains(num))
        {
            num = Random.Range(min, max);
        }

        BulletIndex.Add(num);

        return num;
    }
}
