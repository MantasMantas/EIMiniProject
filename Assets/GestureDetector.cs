using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent OnRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.1f;
    public bool debugMode = true;

    public OVRSkeleton skeleton;
    private List<OVRBone> fingerBones;
    public List<Gesture> gestures;

    private Gesture previourGesture;

    bool isSetUp = false;


    // Start is called before the first frame update
    void Start()
    {
        previourGesture = new Gesture();

    }

    // Update is called once per frame
    void Update()
    {
        if(!isSetUp)
        {
            if(skeleton.IsInitialized)
            {
                fingerBones = new List<OVRBone>(skeleton.Bones);
                isSetUp = true;
                Debug.Log("Just got set up and ready to go!!!");
            }

            return;
        }
        
        if(debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());

        if(hasRecognized && !currentGesture.Equals(previourGesture))
        {
            Debug.Log("New Gesture Found : "+ currentGesture.name);
            previourGesture = currentGesture;
            currentGesture.OnRecognized.Invoke();
        }
        
    }

    void Save()
    {
        Gesture g = new Gesture();
        g.name  = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach(var bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        g.fingerData = data;
        gestures.Add(g);
    }

    Gesture Recognize()
    {
        Gesture currentGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach(var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for(int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerData[i]);
                if(distance > threshold)
                {
                    isDiscarded = true;
                    break;
                }

                sumDistance += distance;
            }

            if(!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentGesture = gesture;
            }
        }

        return currentGesture;
    }
}
