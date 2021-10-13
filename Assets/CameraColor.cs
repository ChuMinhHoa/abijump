using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour
{
    [SerializeField] float timeChange;
    [SerializeField] float timeChangeSetting;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (timeChange <= 0)
        {
            ChangeColor();
            timeChange = timeChangeSetting;
        }
        else timeChange -= Time.deltaTime;
    }

    void ChangeColor() {
        float r = 0, g = 0, b = 0;
        r = Random.Range(0, 122)/255f;
        g = Random.Range(0, 122)/255f;
        b = Random.Range(0, 122)/255f;
        Color newColor = new Color(r, g, b);
        cam = GetComponent<Camera>();
        cam.backgroundColor = newColor;
    }
}
