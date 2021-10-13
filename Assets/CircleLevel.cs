using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLevel : MonoBehaviour
{
    [SerializeField] MeshCollider[] meshColliders;
    [SerializeField] Color colorSetting;
    public bool[] meshDanger;
    private void Start()
    {
        meshColliders = GetComponentsInChildren<MeshCollider>();
        
        for (int i = 0; i < meshColliders.Length; i++)
        {
            meshColliders[i].GetComponent<MeshRenderer>().material.color = colorSetting;

        }
        if (meshColliders.Length == 8)
        {
            int space = Random.Range(1, 4);
            for (int i = 0; i < space; i++)
            {
                int indexSpace = Random.Range(0, 8);
                Destroy(meshColliders[indexSpace].gameObject);

            }
        }
        int dangerCount = Random.Range(0, 3);

        for (int i = 0; i < dangerCount; i++)
        {
            int dangerIndex = Random.Range(0, meshColliders.Length);
            meshColliders[dangerIndex].tag = "Danger";
            ChangeColorDanger(meshColliders[dangerIndex]);
        }

    }

    private void Update()
    {
        updateRederer();
    }
    void updateRederer() {
        meshColliders = GetComponentsInChildren<MeshCollider>();
        meshDanger = new bool[meshColliders.Length];
        for (int i = 0; i < meshColliders.Length; i++)
        {
            if (meshColliders[i].tag=="Danger")
            {
                meshDanger[i] = true;
            }
        }
    }

    void ChangeColorDanger(MeshCollider meshcollider) {
        Color oldColor = meshcollider.GetComponent<MeshRenderer>().material.color;
        Color newColor = new Color(255-(oldColor.r*255)/255f, 255 - (oldColor.g * 255) / 255f, 255 - (oldColor.b * 255) / 255f);
        meshcollider.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void AddRigidbody() {
        meshColliders = GetComponentsInChildren<MeshCollider>();
        for (int i = 0; i < meshColliders.Length; i++)
        {
            Rigidbody meshBody = meshColliders[i].GetComponent<Rigidbody>();
            if (meshColliders[i]!=null && meshBody==null)
            {
                meshColliders[i].gameObject.AddComponent<Rigidbody>();
                Destroy(meshColliders[i].gameObject.GetComponent<Collider>(), 0.2f);
                meshColliders[i].tag = "Untagged";
                Destroy(gameObject, .5f);
            }
           
        }
    }


}
