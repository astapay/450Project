using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class ConeController : MonoBehaviour
{
    [SerializeField] private GameObject[] scoops;
    [SerializeField] private ScoreController scoreController;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Scoop") && !collision.gameObject.GetComponent<ScoopController>().isInStack())
        {
            if (confirmCorrectScoop(collision))
            {
                addToStack(collision.gameObject);
                if (isFullStack())
                {
                    scoreController.addStackPoints();
                    clearStack();
                }
                else
                {
                    scoreController.addScoopPoints();
                }
            }
            else
            {
                scoreController.removeScoopPoints();
                Destroy(collision.gameObject);
            }
        }
    }

    public bool confirmCorrectScoop(Collision2D collision)
    {
        GameController gm = FindObjectOfType<GameController>();
        GameController2 gm2 = FindObjectOfType<GameController2>();

        int lastScoopIndex = 3;

        for (int i = 0; i < scoops.Length; ++i)
        {
            if (scoops[i] == null)
            {
                lastScoopIndex = i - 1;
                break;
            }
        }

        int prevScoopType = -1;
        int nextScoopType = collision.gameObject.GetComponent<ScoopController>().getScoopType();

        if (lastScoopIndex >= 0)
        {
            prevScoopType = scoops[lastScoopIndex].GetComponent<ScoopController>().getScoopType();
        }

        if(gm != null)
        {
            return gm.checkNextScoop(prevScoopType, nextScoopType);
        }
        else if(gm2 != null)
        {
            return gm2.checkNextScoop(prevScoopType, nextScoopType);
        }
        else
        {
            return true;
        }
    }

    public void addToStack(GameObject scoop)
    {
        scoop.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        scoop.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //if less than 4 scoops, add a scoop to the top
        if (!isFullStack())
        {
            for (int i = 0; i < scoops.Length; ++i)
            {
                if (scoops[i] == null)
                {
                    scoops[i] = scoop;
                    scoops[i].transform.position = new Vector3(transform.position.x, scoopPositions[i] + transform.position.y, 0);
                    break;
                }
            }
        }
        else //if 4 scoops, add new scoop to top and remove bottom scoop, moving everything down
        {
            Destroy(scoops[0]);
            for (int i = 1; i < scoops.Length; ++i)
            {
                scoops[i - 1] = scoops[i];
                scoops[i - 1].transform.position = new Vector3(transform.position.x, scoopPositions[i - 1] + transform.position.y, 0);
            }

            scoops[3] = scoop;
            scoops[3].transform.position = new Vector3(transform.position.x, scoopPositions[3] + transform.position.y, 0);
        }
        scoop.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        scoop.transform.parent = transform;        
        scoop.GetComponent<ScoopController>().addToScoop();
    }

    public bool isFullStack()
    {
        return scoops[3] != null;
    }

    public void clearStack()
    {
        for (int i = 0; i < scoops.Length; i++)
        {
            Destroy(scoops[i]);
        }
        scoops = new GameObject[4];

        GameController gm = FindObjectOfType<GameController>();
        GameController2 gm2 = FindObjectOfType<GameController2>();

        if(gm != null)
        {
            if (gm.getGameType())
            {
                gm.CreateOrder();
            }
            else
            {
                gm.endOneOrderGame();
            }
        }
        else if(gm2 != null)
        {
            if (gm2.getGameType())
            {
                gm2.CreateOrder();
            }
            else
            {
                gm2.endOneOrderGame();
            }
        }
    }
}
