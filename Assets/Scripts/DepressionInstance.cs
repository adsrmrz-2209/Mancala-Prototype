using BoardGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepressionInstance : MonoBehaviour
{
    public Depression depression;
    public int startingNumOfShells;
    //public int numOfCurrentShells;
    public TextMeshProUGUI shellNumbersTMPro;

    private void Awake()
    {
        depression = ScriptableObject.CreateInstance<Depression>();
        depression.Shells = startingNumOfShells;

        //depression = ScriptableObject.CreateInstance<Depression>();
        //depression.Steal(OpposingDepressionSrc.depression);
    }


    // Update is called once per frame
    void Update()
    {
        //numOfCurrentShells = depression.Shells;
        shellNumbersTMPro.text = $"{depression.Shells}";
    }
}
