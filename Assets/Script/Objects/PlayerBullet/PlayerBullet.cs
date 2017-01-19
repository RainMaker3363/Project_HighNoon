using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    private GameState State;
    private Player m_Player;

    private Vector3 MoveDir;

	// Use this for initialization
	void Start () {
        State = GameManager.NowGameState;

        MoveDir = Vector3.zero;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            MoveDir = m_Player.GetPlayerDirection();

        }
        else
        {
            MoveDir = m_Player.GetPlayerDirection();

        }

        //this.transform.Rotate(new Vector3(-90.0f, 0.0f, 0.0f));

        StopCoroutine(DeadProtocol(true));
        StartCoroutine(DeadProtocol(true));

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
	}

    void OnEnable()
    {
        State = GameManager.NowGameState;

        MoveDir = Vector3.zero;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            MoveDir = m_Player.GetPlayerDirection();
        }
        else
        {
            MoveDir = m_Player.GetPlayerDirection();
        }

        StopCoroutine(DeadProtocol(true));
        StartCoroutine(DeadProtocol(true));
    }

    IEnumerator DeadProtocol(bool On = true)
    {
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        State = GameManager.NowGameState;

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    this.transform.Translate((new Vector3(MoveDir.x, 0.0f, MoveDir.z) * 20.0f * Time.deltaTime));
                }
                break;

            case GameState.PAUSE:
                {

                }
                break;

            case GameState.EVENT:
                {

                }
                break;

            case GameState.GAMEOVER:
                {

                }
                break;

            case GameState.VICTORY:
                {

                }
                break;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }
}
