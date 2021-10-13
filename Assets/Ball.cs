using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] Color colorChange;
    Rigidbody mybody;
    [SerializeField] float jumpForce;

    [SerializeField] float timeChange;
    [SerializeField] float timeChangeSetting;
    [SerializeField] float sphereRadius;
    [SerializeField] LayerMask whatIsGround;

    [SerializeField] GameObject effectPrefag;
    private void Awake()
    {
        mybody = GetComponent<Rigidbody>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

        Collider[] hit = Physics.OverlapSphere(transform.position,sphereRadius,whatIsGround);
        if (hit.Length>0)
        {
                Vector3 newPos = transform.position;
                float distance = (hit[0].transform.position.y+0.1f) - transform.position.y;
                newPos.y -= distance;
                GameObject effect = Instantiate(effectPrefag,newPos, effectPrefag.transform.rotation);
                Destroy(effect, 0.1f);
        }
    }
    MeshRenderer mesh;
    void ChangeColor()
    {
        float r = 0, g = 0, b = 0;
        r = Random.Range(0, 122) / 255f;
        g = Random.Range(0, 122) / 255f;
        b = Random.Range(0, 122) / 255f;
        Color newColor = new Color(r, g, b);
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = newColor;

        GetComponentInChildren<TrailRenderer>().startColor = newColor;

        var Cylinders = FindObjectsOfType<CircleLevel>();
        for (int i = 0; i < Cylinders.Length; i++)
        {
            MeshRenderer[] mesRens = Cylinders[i].GetComponentsInChildren<MeshRenderer>();
            bool[] danger = Cylinders[i].GetComponentInChildren<CircleLevel>().meshDanger;
            if (danger.Length != 0)
            {


                for (int j = 0; j < mesRens.Length; j++)
                {
                    if (!danger[j])
                    {
                        mesRens[j].material.color = newColor;
                    }


                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Jump();
        }
        if (collision.gameObject.tag == "Danger")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="drop")
        {
            other.GetComponent<CircleLevel>().AddRigidbody();
        }
        if (other.tag == "UpLevel")
        {
            other.GetComponentInParent<Cylinder>().UpLevel();
        }
        if (other.gameObject.tag == "Ground")
        {
            Jump();
        }
    }

    void Jump() {

        mybody.velocity = new Vector3(0,jumpForce,0);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }
}
