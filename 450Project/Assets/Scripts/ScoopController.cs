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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (collision.gameObject.name.Contains("Scoop") && !collision.gameObject.GetComponent<ScoopController>().isInStack() && gameObject.GetComponent<ScoopController>().isInStack())
        {
            ConeController cone = GameObject.FindObjectOfType<ConeController>();
            cone.addToStack(collision.gameObject);
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
