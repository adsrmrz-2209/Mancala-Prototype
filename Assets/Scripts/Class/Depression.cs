using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    [System.Serializable]
    public class Depression : ScriptableObject
    {
        public Depression(/*Depression opposingDepression*/)
        {
            //this.OpposingDepression = opposingDepression;
            shells = 7;
        }

        //[SerializeField]
        //private Depression opposingDepression;

        //public Depression OpposingDepression
        //{
        //    get { return opposingDepression; }
        //    set { opposingDepression = value; }
        //}

        //public void SetOpposingDepression(Depression opposingDepression)
        //{
        //    this.OpposingDepression = opposingDepression;
        //}

        [SerializeField]
        private int shells;
        public int Shells
        {
            get { return shells; }
            set
            {
                //limit or clamp the shells to never go below 0
                shells = value < 0 ? 0 : value;
                //shells = Mathf.Clamp(value, 0, shells);
            }
        }



        //better to use this method to utilize the clamping of the Shells value
        public int IncrementShells()
        {
            return Shells++;
        }

        //better to use this method to utilize the clamping of the Shells value
        public int DecrementShells()
        {
            return Shells--;
        }

       

    }

}



