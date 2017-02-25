using UnityEngine;
using System.Collections;

public class PietaEnemyBullet : MonoBehaviour {

    private GameState State;
    public Player m_Player;

    private Vector3 MoveDir;

	// Use this for initialization
	void Start () {
        State = GameManager.NowGameState;

        MoveDir = Vector3.zero;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            MoveDir = (m_Player.gameObject.transform.position - this.transform.position).normalized;

        }
        else
        {
            MoveDir = (m_Player.gameObject.transform.position - this.transform.position).normalized;

        }

        //this.transform.Rotate(new Vector3(-90.0f, 0.0f, 0.0f));


        StopCoroutine(DeadProtocol(true));
        StartCoroutine(DeadProtocol(true));

        
	}

    void OnEnable()
    {
        State = GameManager.NowGameState;

        MoveDir = Vector3.zero;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            MoveDir = (m_Player.gameObject.transform.position - this.transform.position).normalized;

        }
        else
        {
            MoveDir = (m_Player.gameObject.transform.position - this.transform.position).normalized;

        }


        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Item"), true);

        StopCoroutine(DeadProtocol(true));
        StartCoroutine(DeadProtocol(true));
    }

    IEnumerator DeadProtocol(bool On = true)
    {
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);

    }

    public void SetMoveDir(Vector3 Dir)
    {
        MoveDir = (Dir - this.transform.position).normalized;
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
                    this.transform.Translate((new Vector3(MoveDir.x, 0.0f, MoveDir.z) * 15.0f * Time.deltaTime));
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
        if (collision.transform.tag.Equals("Player"))
        {
            this.gameObject.SetActive(false);
        }

        if (collision.transform.tag.Equals("Wall"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
