using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour {

    public GameObject rabbits;
    public GameObject door;
    GameObject sphere;

    bool started;
	// Use this for initialization
	void Start () {
        started = false;
        sphere = GameObject.Find("LightSphere");
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !started)
        {
            started = true;
            StartCoroutine(DanceCoroutine());
            StartCoroutine(LightOnCoroutine());
        }
    }

    private IEnumerator DanceCoroutine()
    {
        sphere.SetActive(false);
        door.SetActive(true);
        //play inception here
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        rabbits.SetActive(true);
        yield return null;
    }
    private IEnumerator LightOnCoroutine()
    {
        yield return new WaitForSeconds(5);
        sphere.SetActive(true);
        rabbits.GetComponent<AudioSource>().Play();
        foreach (Animation a in GetComponentsInChildren<Animation>())
        {
            a.CrossFade("Scene");
        }
    }
}
