using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    
    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private Transform prevObject;
    [SerializeField] private Transform parent;

    public List<GameObject> picked = new List<GameObject>();
    public static StackManager instance;
    public static int pickCount;
    private void Awake()
    {
    
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        distanceBetweenObjects = prevObject.localScale.y;
    }

    public void PickUp(GameObject pickedObject, bool needTag = false, string tag = null)
    {
        if (needTag)
        {
            pickedObject.tag = tag;
        }
        picked.Add(pickedObject);
        pickCount++;

        pickedObject.transform.parent = parent;
        
        Vector3 desPos = prevObject.localPosition;
        
        desPos.y +=distanceBetweenObjects;

        pickedObject.transform.localPosition = desPos;
        prevObject = pickedObject.transform;
        
    }
}
