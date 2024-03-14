using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject scoopPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnScoop", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnScoop()
    {
        Vector3 spawnPos = new Vector3(0, 6, 0);

        spawnPos.x = Random.Range(-7.62f, 7.62f);

        Instantiate(scoopPrefab, spawnPos, Quaternion.identity);
    }
}
