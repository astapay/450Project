using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoopController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private int scoopType;
    private bool inStack = false;

    // Start is called before the first frame update
    void Start()
    {
        scoopType = Random.Range(0, 5);

        // Set ice cream sprite
        GetComponent<SpriteRenderer>().sprite = sprites[scoopType];
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (collision.gameObject.name.Contains("Scoop") && !collision.gameObject.GetComponent<ScoopController>().isInStack() && gameObject.GetComponent<ScoopController>().isInStack())
        {
            ConeController cone = GameObject.FindObjectOfType<ConeController>();
            ScoreController scoreController = FindObjectOfType<ScoreController>();

            if (cone.confirmCorrectScoop(collision))
            {
                cone.addToStack(collision.gameObject);
                if (cone.isFullStack())
                {
                    scoreController.addStackPoints();
                    cone.clearStack();
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

    public void addToScoop()
    {
        inStack = true;
    }

    public bool isInStack()
    {
        return inStack;
    }

    public int getScoopType()
    {
        return scoopType;
    }
}
