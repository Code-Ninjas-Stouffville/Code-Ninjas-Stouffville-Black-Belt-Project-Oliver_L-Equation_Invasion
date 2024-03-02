using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{       
    public GameObject background;

    private void OnTriggerEnter(Collider other)
    {
        background.GetComponent<GameManager>().moveToCheckPoint();
    }
}
