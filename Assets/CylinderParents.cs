using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderParents : MonoBehaviour
{
    public Cylinder[] Cylinders;
    public  int cylinderCount;
    [SerializeField] GameObject cylinderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cylinders.Length< cylinderCount)
        {
            CreateCylinder();
        }
    }

    void CreateCylinder() {
        Cylinders = GetComponentsInChildren<Cylinder>();
        Transform lastTransform = Cylinders[Cylinders.Length - 1].transform;
        Vector3 newPosition = lastTransform.position;
        newPosition.y -= (lastTransform.localScale.y * 2);

        Instantiate(cylinderPrefab, newPosition, Quaternion.identity, transform);
        Cylinders = GetComponentsInChildren<Cylinder>();
    }
}
