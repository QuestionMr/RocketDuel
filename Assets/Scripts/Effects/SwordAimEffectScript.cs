using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAimEffectScript : AimEffectScript
{
    public Mesh m_aimMesh;
    private Vector3[] m_vertices;
    private int[] m_triangles;
    private MeleeWP m_meleeScript;
    public MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    protected override void Awake(){

    }
    protected override void Start()
    {
        m_vertices = new Vector3[11];
        m_triangles = new int[27];
        m_aimMesh = new Mesh();
        m_meshFilter.mesh = m_aimMesh;
        m_meleeScript = GetComponent<MeleeWP>();
        m_meshRenderer = GetComponentInChildren<MeshRenderer>();
        Vector3 temp = new Vector3(0,0,-4);
        DrawCone(temp, m_meleeScript.m_meleeRadius, transform.right, m_meleeScript.m_angleOffset, 10);
        SetEffectVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetEffectVisibility(bool enabled){
        //Debug.Log(enabled);
        if (m_meshRenderer.enabled == enabled) return;
        m_meshRenderer.enabled = enabled;
    }
    void LateUpdate(){
        //DrawCone(Vector2.zero, 10f, Vector2.up, m_meleeScript.m_angleOffset, 10);

    }

    public override void SetEffectDirection(Vector3 dir)
    {
        //DrawCone(Vector2.zero, m_meleeScript.m_meleeRadius, dir, m_meleeScript.m_angleOffset, 10);
    }
    public void DrawCone(Vector2 origin, float radius, Vector2 direction, float angleOffset, int numOfRay = 2){
        direction = direction.normalized * radius;
        direction.x /= transform.localScale.x;
        direction.y /= transform.localScale.y;
        numOfRay = Mathf.Max(numOfRay, 2);
        float rayAngleOffset = angleOffset * 2 / (numOfRay - 1);
        float startingAngle = angleOffset;
        Vector2 currentRay;
        m_vertices[0] = origin;
        for (int i = 1; i <= numOfRay; i++){
            currentRay = Quaternion.AngleAxis(startingAngle, Vector3.forward) * direction;
            m_vertices[i] = origin + currentRay;
            startingAngle -= rayAngleOffset;
        }    
        for (int i = 0; i < numOfRay - 1; i++){
            m_triangles[i * 3] = 0;
            m_triangles[i * 3 + 1] = i + 1;
            m_triangles[i * 3 + 2] = i + 2;
        }
        m_aimMesh.Clear();
        m_aimMesh.vertices = m_vertices;
        // foreach (Vector3 vert in m_aimMesh.vertices){
        //     Debug.DrawLine(origin, vert, Color.red);
        // }
        m_aimMesh.triangles = m_triangles;
        // for (int i = 0; i < m_aimMesh.triangles.Length; i+=3){
        //     Debug.DrawLine(m_aimMesh.vertices[m_aimMesh.triangles[i +1]], m_aimMesh.vertices[m_aimMesh.triangles[i+2]],Color.green);
        // }
        // foreach (Vector3 vert in m_aimMesh.vertices) Debug.Log(vert);
        // foreach (int tr in m_aimMesh.triangles) Debug.Log(tr);
        m_aimMesh.RecalculateNormals();
                            
    }
}
