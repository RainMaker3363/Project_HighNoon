using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Single_RevolverTouch : MonoBehaviour {

    public static int BulletChecker;
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
    }
	
	// Update is called once per frame
	void Update () {
	
        if(BulletChecker > Bullets.Length)
        {
            GameManager.DeadEyeActiveOn = false;
            GameManager.DeadEyeVersusAction = false;
            GameManager.DeadEyeRevolverAction = false;

            this.gameObject.SetActive(false);
        }

        //print("BulletChecker : " + BulletChecker);
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
