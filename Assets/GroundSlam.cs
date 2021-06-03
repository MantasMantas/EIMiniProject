using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    public GameObject hand;
    public ParticleSystem preEffect;
    public ParticleSystem mainEffect;


    bool rightHand = false;
    bool leftHand = false;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftHand && rightHand)
        {
            preEffect.Play();
            
            if(hand.transform.position.y <= 0.6)
            {
                if(!mainEffect.isPlaying)
                {
                    mainEffect.Play();
                    sound.Play();
                }
            }
            
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
