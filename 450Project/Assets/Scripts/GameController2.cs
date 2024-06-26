using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController2 : MonoBehaviour
{
    [SerializeField] private GameObject scoopPrefab;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private TMP_Text timerText;

    private List<int> flavorOrder;
    private bool multipleOrdersGame;

    // Start is called before the first frame update
    void Start()
    {
        multipleOrdersGame = false;

        CreateOrder();

        if (multipleOrdersGame)
        {
            InvokeRepeating("SpawnScoop", 1, 1);

            timerText.text = ":59";
            StartCoroutine(timerDown());
        }
        else
        {
            InvokeRepeating("SpawnScoop", 0.5f, 0.5f);

            timerText.text = ":0";
            StartCoroutine(timerUp());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateOrder()
    {
        flavorOrder = new List<int>();

        HashSet<int> usedNums = new HashSet<int>();
        string flavorOrderText = "";

        while (usedNums.Count < 5)
        {
            int thisScoop = Random.Range(0, 5);

            if (!usedNums.Contains(thisScoop))
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
        flavorOrderText = flavorOrderText.Substring(0, flavorOrderText.Length - 2);

        orderText.text = "" + flavorOrderText;
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

    IEnumerator timerDown()
    {
        for (int i = 59; i >= 0; i--)
        {
            timerText.text = ":"+i;
            yield return new WaitForSeconds(1);
        }
        sceneController.endGame(-999);
    }

    IEnumerator timerUp()
    {
        for (int i = 0; i >= 0; i++)
        {
            timerText.text = ":" + i;
            yield return new WaitForSeconds(1);
        }
    }

    public void flipGameType()
    {
        multipleOrdersGame = false;
    }

    public bool getGameType()
    {
        return multipleOrdersGame;
    }

    public void endOneOrderGame()
    {
        sceneController.endGame(int.Parse(timerText.text.Substring(1)));
    }
}
