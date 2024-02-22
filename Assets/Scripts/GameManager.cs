using BoardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Animator anim;
    public PlayerSide playerSide;
    public int playerA_Score;
    public int playerB_Score;

    public TextMeshProUGUI playerA_Home;
    public TextMeshProUGUI playerB_Home;

    public List<Transform> depressions;
    public List<DepressionManager> depressionManagers;
    public Transform indicator;

    //the limiter or to know how many shells is collected and to know how many moves the indicator will move
    public int shellsCollected;

    //starting position of the indicator
    public int startingIndex;

    public List<Button> playerA_Buttons;
    public List<Button> playerB_Buttons;
    public List<Button> All_Buttons;


    void Start()
    {
        playerSide = PlayerSide.PlayerA;
        anim = anim.GetComponent<Animator>();

        foreach(Button btns_A in playerA_Buttons)
        {
            All_Buttons.Add(btns_A);
        }

        foreach (Button btns_B in playerB_Buttons)
        {
            All_Buttons.Add(btns_B);
        }
    }


    void Update()
    {
        
        playerA_Home.text = $"{playerA_Score}";
        playerB_Home.text = $"{playerB_Score}";
    }

    public void SetStartingIndex(int index)
    {
        startingIndex = index;
    }

    public void StartMoving()
    {
        StartCoroutine(Delay());
    }

    public void DetectPlayer()
    {
        switch (playerSide)
        {
            case PlayerSide.PlayerA:
                anim.SetBool("isPlayerA", true);
                foreach (Button button in playerA_Buttons)
                {
                    button.interactable = true;
                }
                foreach (Button button in playerB_Buttons)
                {
                    button.interactable = false;
                }
                break;


            case PlayerSide.PlayerB:
                anim.SetBool("isPlayerA", false);
                foreach (Button button in playerB_Buttons)
                {
                    button.interactable = true;
                }
                foreach (Button button in playerA_Buttons)
                {
                    button.interactable = false;
                }
                break;

            default:
                break;
        }
    }

    void AreYouWinningSon()
    {
        bool allIsEmpty = false;

        foreach(var manager in depressionManagers)
        {
            if(manager.depressionA.depression.Shells != 0 && manager.depressionB.depression.Shells != 0)
            {
                allIsEmpty = false;
            }
            if (allIsEmpty)
            {
                Debug.Log(playerA_Score > playerB_Score ? "PLAYER BLUE WINS" : "PLAYER RED WINS");
            }
        }
    }

    public void TryAgain()
    {
        shellsCollected = 0;
        playerA_Score = 0;
        playerB_Score = 0;
        for (int i = 0; i < All_Buttons.Count; i++)
        {
            All_Buttons[i].gameObject.GetComponentInParent<MakeMove>().depressionInstance.depression.Shells = 7;
        }
        playerSide = PlayerSide.PlayerA;
        DetectPlayer();
    }

    public void EndTurn()
    {
        playerSide = playerSide == PlayerSide.PlayerA ? PlayerSide.PlayerB : PlayerSide.PlayerA;
        DetectPlayer();
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Delay()
    {
        for (int i = startingIndex + 1; shellsCollected != 0;  i++)
        {
            //to cycle through the List<Transform> depressions when it reaches the last depressions[i]
            int index = i % depressions.Count;

            //move the indicator to the next depression
            indicator.transform.position = depressions[index].transform.position;

            if (index == 7)
            {
                playerA_Score++;
            }
            else if (index == 15)
            {
                playerB_Score++;
            }
            else
            {
                //increment the depressions that are passed by the indicator
                depressions[index].gameObject.GetComponentInParent<MakeMove>().depressionInstance.depression.IncrementShells();

                //for (int j = 0; j < depressionManagers.Count; j++)
                //{
                //    //check to see if each sides can steal
                //    depressionManagers[j].Steal();
                //}

                //check to see if each sides can steal. This is better than the for loop above
                depressions[index].gameObject.GetComponentInParent<DepressionManager>().Steal();
            }

            //decrement the collected shells each move of the indicator
            shellsCollected--;

            yield return new WaitForSeconds(0.7f);

            if (shellsCollected == 0)
            {
                foreach(Button button in All_Buttons)
                {
                    button.interactable =  true;
                }

                if (index <= 7)
                {
                    playerSide = PlayerSide.PlayerA;
                    DetectPlayer();

                }
                else if(index >= 8)
                {
                    playerSide = PlayerSide.PlayerB;
                    DetectPlayer();

                }

                AreYouWinningSon();
            }

        }

    }
}

public enum PlayerSide
{
    PlayerA,
    PlayerB
}
