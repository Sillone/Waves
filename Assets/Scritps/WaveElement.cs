using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveElement : MonoBehaviour
{
    public int i;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Waves" && other.gameObject.tag != "Player")
        {
            
            print("killme");
            gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<WavesScr>().KillMe(i);
        }
        print(other.gameObject.tag.ToString());
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Waves" && other.gameObject.tag != "Player")
        {
            print("killme");
            gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<WavesScr>().KillMe(i);
        }
        print(other.gameObject.tag.ToString());
    }
}
