using UnityEngine;
using System.Collections;

public class MulEnemy : MonoBehaviour {

    public MulEnemy_Ani m_EnemyAni;

    private GameState NowGameState;
    private AnimationState EnemyAniState;

    private Vector3 _startPos;
    private string InfoText;

	// Use this for initialization
	void Start () {
	
        if(m_EnemyAni == null)
        {
            m_EnemyAni = GameObject.Find("EnemyLincoin_Ani").GetComponent<MulEnemy_Ani>();
        }

        _startPos = this.transform.position;

        NowGameState = MultiGameManager.NowGameState;
	}
	
	// Update is called once per frame
	void Update () {
        NowGameState = MultiGameManager.NowGameState;

        switch(NowGameState)
        {
            case GameState.PLAY:
                {

                }
                break;

            case GameState.PAUSE:
                {

                }
                break;

            case GameState.START:
                {

                }
                break;

            case GameState.EVENT:
                {

                }
                break;

            case GameState.VICTORY:
                {

                }
                break;

            case GameState.GAMEOVER:
                {

                }
                break;

        }
	}

    public AnimationState GetEnemyAniState()
    {
        return EnemyAniState;
    }

    public void SetTransformInformation(float posX, float posY, float velX, float velY, float rotZ)
    {
        _startPos = this.transform.position;

        transform.position = new Vector3(posX, 0, posY);

        transform.rotation = Quaternion.Euler(0, rotZ, 0);
        // We're going to do nothing with velocity.... for now
    }

}
