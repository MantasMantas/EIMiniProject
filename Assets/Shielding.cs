using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielding : MonoBehaviour
{
    public GameObject shield;

    bool rightHand = false;
    bool leftHand = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(leftHand && rightHand)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
        
    }

    public void toggleLeft()
    {
        leftHand = !leftHand;
    }

    public void toggleRight()
    {
        rightHand = !rightHand;
    }
}
