using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject avatar;
    public GameObject hand;
    public GameObject pointer;

    private bool isEnabled = false;
    private bool teleport = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabled)
        {
            if(!pointer.activeSelf)
            {
                pointer.SetActive(true);
            }

            Ray ray = new Ray(hand.transform.position, hand.transform.rotation * Vector3.forward);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 5, 1 << 6))
            {
                pointer.transform.position = hit.point;
            }
        }

        else
        {
            if(pointer.activeSelf)
            {
                pointer.SetActive(false);
            }
        }
        
    }

    public void toggleEnable()
    {
        isEnabled = !isEnabled;
    }

    public void Teleport()
    {
        if(isEnabled)
        {
            avatar.transform.position = new Vector3(pointer.transform.position.x, avatar.transform.position.y, pointer.transform.position.z);
            toggleEnable();
        }
    }
}
