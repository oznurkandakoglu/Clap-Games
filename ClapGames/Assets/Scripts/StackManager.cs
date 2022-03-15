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
        picked.Add(prevObject.gameObject);
    }

    public void PickUp(GameObject pickedObject, bool needTag = false, string tag = null)
    {
        if (needTag)
        {
            pickedObject.tag = tag;
        }

        Vector3 desPos = picked[picked.Count - 1].transform.localPosition;

        picked.Add(pickedObject);
        pickCount++;

        pickedObject.transform.parent = parent; 
        desPos.y +=distanceBetweenObjects;
        pickedObject.transform.localPosition = desPos;
             
    }
}
