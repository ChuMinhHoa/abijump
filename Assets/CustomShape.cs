using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShape : MonoBehaviour
{

    private void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        float fov = 45f;

        int rayCount = 50;

        float angle = 0f;

        float angleIncrease = fov / rayCount;
        float viewDistance = 1f;

        Vector3 origin = Vector3.zero;

        Vector3[] vertices = new Vector3[rayCount+1+1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3+3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        vertices[vertices.Length - 3] = new Vector3(0, 0, 0);
        vertices[vertices.Length - 2] = new Vector3(0, 1, 0);
        vertices[vertices.Length - 1] = new Vector3(1, 1, 0);


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        
        

    }

 

    Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0 , Mathf.Sin(angleRad));
    }
}
