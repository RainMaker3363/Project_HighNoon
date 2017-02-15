using UnityEngine;
using System.Collections;

public class MulEnemy : MonoBehaviour {

    public MulEnemy_Ani m_EnemyAni;

    private AnimationState EnemyAniState;

	// Use this for initialization
	void Start () {
	
        if(m_EnemyAni == null)
        {
            m_EnemyAni = GameObject.Find("EnemyLincoin_Ani").GetComponent<MulEnemy_Ani>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public AnimationState GetEnemyAniState()
    {
        return EnemyAniState;
    }

    public void SetTransformInformation(float posX, float posY, float velX, float velY, float rotZ)
    {
        transform.position = new Vector3(posX, posY, 0);
        //transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // We're going to do nothing with velocity.... for now
    }

}
