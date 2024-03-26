using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustEmptyBox : MonoBehaviour
{
    public Sprite OpenedBox;
    public GameObject Box;
    public CollectPowerUp opening;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && opening.hasOpened == false)
        {
            Box.gameObject.GetComponent<SpriteRenderer>().sprite = OpenedBox;

        }
    }
}
