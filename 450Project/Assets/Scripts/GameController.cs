using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject scoopPrefab;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text orderText;

    private int score;
    private List<int> flavorOrder;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        flavorOrder = new List<int>();

        HashSet<int> usedNums = new HashSet<int>();
        string flavorOrderText = "";

        while(usedNums.Count < 5)
        {
            int thisScoop = Random.Range(0, 5);

            if(!usedNums.Contains(thisScoop))
            {
                usedNums.Add(thisScoop);
                flavorOrder.Add(thisScoop);

                switch (thisScoop)
                {
                    case 0:
                        flavorOrderText += "V";
                        break;
                    case 1:
                        flavorOrderText += "C";
                        break;
                    case 2:
                        flavorOrderText += "S";
                        break;
                    case 3:
                        flavorOrderText += "M";
                        break;
                    default:
                        flavorOrderText += "B";
                        break;
                }

                flavorOrderText += "->";
            }
        }

        orderText.text = "Order: " + flavorOrderText;

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

    public bool checkNextScoop(int prevScoopType, int nextScoopType)
    {
        if (prevScoopType == -1)
        {
            return true;
        }

        for(int i = 0; i < flavorOrder.Count; ++i)
        {
            if (flavorOrder[i] == prevScoopType)
            {
                if(i == flavorOrder.Count - 1)
                {
                    return flavorOrder[0] == nextScoopType;
                }
                else
                {
                    return flavorOrder[i+1] == nextScoopType;
                }
            }
        }

        return false;
    }

    public void updateScore()
    {
        ++score;

        scoreText.text = "Score: " + score;
    }
}
