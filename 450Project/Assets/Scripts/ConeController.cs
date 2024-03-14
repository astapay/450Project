using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{
    [SerializeField] private GameObject[] scoops;

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
        // Set new scoop on cone
        if(collision.gameObject.name.Contains("Scoop"))
        {
            if (scoops[3] == null)
            {
                for (int i = 0; i < scoops.Length; ++i)
                {
                    if (scoops[i] == null)
                    {
                        scoops[i] = collision.gameObject;
                    }
                }
            }
            else
            {
                for(int i = 1; i < scoops.Length; ++i)
                {
                    scoops[i - 1] = scoops[i];
                }

                scoops[3] = collision.gameObject;
            }

            Destroy(collision.gameObject);
        }
    }
}
