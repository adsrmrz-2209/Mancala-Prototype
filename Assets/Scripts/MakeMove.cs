using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeMove : MonoBehaviour
{
    public DepressionInstance depressionInstance;
    public GameManager gameManager;
    public int depressionIndex;
    //public Button button;


    // Start is called before the first frame update
    void Start()
    {
        //depressionInstance = depressionInstance.GetComponent<DepressionInstance>();
        //button = this.gameObject.GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //button.interactable = depressionInstance.depression.Shells == 0 ? false : true;
    }

    public void SelectThis()
    {

       
        if(depressionInstance.depression.Shells != 0)
        {
            //temporarily disable the clickable buttons
            foreach (Button btn in gameManager.All_Buttons)
            {
                btn.interactable = false;
            }
            //collect the shells in this depression
            gameManager.shellsCollected += depressionInstance.depression.Shells;
            depressionInstance.depression.Shells = 0;

            //use this depression index as the starting location of the indicator
            gameManager.SetStartingIndex(depressionIndex); // or //gameManager.startingIndex = depressionIndex;
            gameManager.StartMoving();
        }
       
        
    }

}
