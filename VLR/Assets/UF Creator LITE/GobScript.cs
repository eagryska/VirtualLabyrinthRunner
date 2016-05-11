using UnityEngine;
using System.Collections;

public class GobScript : MonoBehaviour {

    private Animation anim;
    private float counter;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
    }

    private IEnumerator MyCoroutine(string name1, string name2)
    {
        yield return new WaitForSeconds(anim.GetClip(name1).length * 2 - 0.3f);
        anim.CrossFade(name2);
        yield return null;
    }

    // Update is called once per frame
    void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           anim.CrossFade("Jump");
           StartCoroutine(MyCoroutine("Jump", "Idle_01"));
        }
    }
}
