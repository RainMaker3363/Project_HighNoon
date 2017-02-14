using UnityEngine;
using System.Collections;

public class MultiPlayerBullet : MonoBehaviour {


    private GameState State;
    private MulPlayer m_Player;

    private Vector3 MoveDir;

    // Use this for initialization
    void Start()
    {
        State = MultiGameManager.NowGameState;

        MoveDir = Vector3.zero;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<MulPlayer>();
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);

        }
        else
        {
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);

        }

        //this.transform.Rotate(new Vector3(-90.0f, 0.0f, 0.0f));

        StopCoroutine(DeadProtocol(true));
        StartCoroutine(DeadProtocol(true));

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Wall"), true);
    }

    void OnEnable()
    {
        State = MultiGameManager.NowGameState;

        MoveDir = Vector3.zero;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<MulPlayer>();
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);
        }
        else
        {
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);
        }

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);

        StopCoroutine(DeadProtocol(true));
        StartCoroutine(DeadProtocol(true));
    }

    IEnumerator DeadProtocol(bool On = true)
    {
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        State = MultiGameManager.NowGameState;

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    this.transform.Translate((new Vector3(MoveDir.x, 0.0f, MoveDir.z) * 40.0f * Time.deltaTime));
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
        if (collision.transform.tag.Equals("Enemy"))
        {
            this.gameObject.SetActive(false);
        }

        if (collision.transform.tag.Equals("Wall"))
        {
            //this.gameObject.SetActive(false);
        }

    }
}
