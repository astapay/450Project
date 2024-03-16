using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{
    [SerializeField] private GameObject[] scoops;
    private readonly float[] scoopPositions = {0.7870655f, 1.309898f, 1.852146f, 2.399546f};

    // Start is called before the first frame update
    void Start()
    {
        scoops = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        // Setting cone pos based on mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        mousePos.y = -3;

        if(mousePos.x < -7.62f)
        {
            mousePos.x = -7.62f;
        }
        else if(mousePos.x > 7.62f)
        {
            mousePos.x = 7.62f;
        }

        transform.position = mousePos;

        // Setting collected scoops as part of cone
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Scoop") && !collision.gameObject.GetComponent<ScoopController>().isInStack())
        {
            addToStack(collision.gameObject);
        }
    }

    public void addToStack(GameObject scoop)
    {
        scoop.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        //if less than 4 scoops, add a scoop to the top
        if (scoops[3] == null)
        {
            for (int i = 0; i < scoops.Length; ++i)
            {
                if (scoops[i] == null)
                {
                    scoops[i] = scoop;
                    scoops[i].transform.position = new Vector3(transform.position.x, scoopPositions[i], 0);
                }
            }
        }
        else //if 4 scoops, add new scoop to top and remove bottom scoop, moving everything down
        {
            Destroy(scoops[0]);
            for (int i = 1; i < scoops.Length; ++i)
            {
                scoops[i].transform.position = new Vector3(transform.position.x, scoopPositions[i - 1], 0);
                scoops[i - 1] = scoops[i];
            }

            scoops[3] = scoop;
        }
        scoop.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        scoop.transform.parent = transform;        
        scoop.GetComponent<ScoopController>().addToScoop();
    }
}