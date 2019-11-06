using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Move(float[] stick)
    {
        if (Joycons.A)
        {
            Vector2 direction = Vector2.zero;

            #region Define a diagonal
            //Upright
            if (stick[0] >= 0 && stick[1] >= 0)
            {
                direction = new Vector3(1, 0.585f);
            }
            //Upleft
            else if (stick[0] < 0 && stick[1] > 0)
            {
                direction = new Vector3(-1, 0.585f);
            }
            //Downright
            else if (stick[0] >= 0 && stick[1] <= 0)
            {
                direction = new Vector3(1, -0.585f);
            }
            //Downleft
            else if (stick[0] < 0 && stick[1] < 0)
            {
                direction = new Vector3(-1, -0.585f);
            }
            #endregion
        }
    }
}
