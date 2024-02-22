using BoardGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DepressionManager : MonoBehaviour
{
    GameManager gameManager;
    public DepressionInstance depressionA;
    public DepressionInstance depressionB;

    Depression depression_A;
    Depression depression_B;

    private int previousShells_A;
    private int previousShells_B;




    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        depression_A = depressionA.GetComponent<DepressionInstance>().depression;
        depression_B = depressionB.GetComponent<DepressionInstance>().depression;
    }


    public bool Can_A_Steal()
    {
        if (depression_A.Shells == 1 && depression_B.Shells >= 1)
        {
            return true;
        }
        else return false;
    }

    public bool Can_B_Steal()
    {
        if (depression_B.Shells == 1 && depression_A.Shells >= 1)
        {
            return true;
        }
        else return false;
    }

    public void Steal()
    {
        //record the previoud number of Shells by assigning it to previousShells_A and previousShells_B
        //this is to track if a depression slot is 0 and is about to become 1 so then it could steal
        previousShells_A = depression_A.Shells;
        previousShells_B = depression_B.Shells;
        if (gameManager.playerSide is PlayerSide.PlayerA)
        {
            if (Can_A_Steal())
            {
                depression_A.Shells += depression_B.Shells;
                depression_B.Shells = 0;
            }
            
        }
        else if (gameManager.playerSide is PlayerSide.PlayerB)
        {
            if (Can_B_Steal())
            {
                depression_B.Shells += depression_A.Shells;
                depression_A.Shells = 0;
            }
            
        }
    }

}

