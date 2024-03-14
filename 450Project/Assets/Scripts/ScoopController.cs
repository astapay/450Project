using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoopController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private int scoopType;

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
}
