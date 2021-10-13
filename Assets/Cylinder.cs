using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] float rotageSpeed;
    [SerializeField] GameObject circleLevelPrefab;
    public CircleLevel[] circleLevels;
    [SerializeField] int levelCount;
    [SerializeField] float distance;

    private void Start()
    {
        CircleLevelController();
    }

    private void Update()
    {
        Vector3 Rotation = Vector3.zero;
        if (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Moved)
        {
            Rotation = Input.GetTouch(0).deltaPosition;
            
        }

        if (Input.GetMouseButton(0))
        {
            float axisXMouse = Input.GetAxis("Mouse X");
            if (axisXMouse != 0)
                Rotation = new Vector3(axisXMouse, 0, 0);
        }
        

        transform.Rotate(0, rotageSpeed * Time.deltaTime * Rotation.x, 0);

        
    }

    void CircleLevelController()
    {
        
        for (int i = 0; i < levelCount; i++)
        {
            CreateLevel();
        }

        
    }

    void CreateLevel() {
        circleLevels = GetComponentsInChildren<CircleLevel>();
        Transform lastLevel = circleLevels[circleLevels.Length - 1].transform;
        Vector3 newPos = new Vector3(lastLevel.position.x, lastLevel.position.y - distance, lastLevel.position.z);
        
        GameObject newLevel = Instantiate(circleLevelPrefab, newPos, Quaternion.identity, gameObject.transform);
        newLevel.transform.Rotate(0,Random.Range(0,181),0);
        
    }

    public void UpLevel() {
        GetComponentInParent<CylinderParents>().cylinderCount++;
        Destroy(gameObject, 1f);
    }
}
