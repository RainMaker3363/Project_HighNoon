using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoText : MonoBehaviour {

    private Text MyText;
    //private IEnumerator ResurectCoroutine;

	// Use this for initialization
	void Start () {
	
        if(MyText == null)
        {
            MyText = GetComponent<Text>();
        }

        //ResurectCoroutine = null;
        //ResurectCoroutine = ResurectProtocol(true);

        //StopCoroutine(ResurectCoroutine);
        //StartCoroutine(ResurectCoroutine);
	}

    void OnEnable()
    {
        if (MyText == null)
        {
            MyText = GetComponent<Text>();
        }

        //ResurectCoroutine = null;
        //ResurectCoroutine = ResurectProtocol(true);

        //StopCoroutine(ResurectCoroutine);
        //StartCoroutine(ResurectCoroutine);
    }


    public void SwitchingOn(int on)
    {
        this.gameObject.SetActive(false);

        //ResurectCoroutine = null;
        //ResurectCoroutine = ResurectProtocol(true);

        //StopCoroutine(ResurectCoroutine);
        //StartCoroutine(ResurectCoroutine);
    }

    //IEnumerator ResurectProtocol(bool on = true)
    //{
    //    yield return new WaitForSeconds(2.0f);

    //    this.gameObject.SetActive(false);
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}
}
