using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusScoreSm : MonoBehaviour
{

    public Scoretext Sm;

    // Start is called before the first frame update
    void Start()
    {
        Sm = GameObject.Find("scorecounttext").GetComponent<Scoretext>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Sm.Score -= 5;
        }
    }
}