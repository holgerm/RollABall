using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExtender : MonoBehaviour
{
    [SerializeField]int size = 20;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject parent;
    int triggerPosition;
    // Start is called before the first frame update
    void Start()
    {
        triggerPosition = size;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= triggerPosition)
        {
            GameObject newGroundObject =  Instantiate(prefab,new Vector3(0, 0, triggerPosition + 2*size), Quaternion.identity);
            newGroundObject.transform.SetParent(parent.transform);
            triggerPosition += size;
        }
    }
}
