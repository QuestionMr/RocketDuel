using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;



public class CameraFollowScript : MonoBehaviour
{
    struct CameraBound
    {
        public Vector3 m_center;
        public float m_zoomSize;
    }
    //public CameraDataSO m_cameraData;
    public CamDataV2 m_cameraData;
    //public CamDataV2 m_cameraDataTest;
    public List<Transform> m_targets;
    public Vector3 m_offset;
    public float m_smoothTime;
    public float m_lerpScale;

    private Camera m_camera;
    private float m_heightToWidthRatio;
    private float m_maxConstraintSize;

    private Vector3 m_cameraVelocity;
    private Bounds m_testBounds;
    private Bounds m_tempBounds;
    void Start(){
        
    }
    public void CameraSetup(){
        m_camera = GetComponent<Camera>();
        float m1 = Screen.currentResolution.height;
        float m2 = Screen.currentResolution.width;
        m_heightToWidthRatio = m1 / m2;//m_camera.rect.height / m_camera.rect.width;
        Debug.Log(m1  + " " + m2 + " " + m_heightToWidthRatio);
        m_tempBounds = m_cameraData.m_cameraConstraints;
        m_tempBounds.size = new Vector2(Mathf.Max(m_tempBounds.size.y / m_heightToWidthRatio, m_tempBounds.size.x),
        Mathf.Max(m_tempBounds.size.x * m_heightToWidthRatio, m_tempBounds.size.y));
        m_tempBounds.center += new Vector3(0, m_tempBounds.size.y - m_cameraData.m_cameraConstraints.size.y) * 0.5f;
        Bounds maxBounds = new Bounds(m_tempBounds.center - new Vector3(0,(m_cameraData.m_extraBottomRatio - m_cameraData.m_extraTopRatio) * m_tempBounds.size.y * 0.5f), 
        m_tempBounds.size + new Vector3(m_tempBounds.size.x * m_cameraData.m_extraSideRatio, m_tempBounds.size.y * (m_cameraData.m_extraBottomRatio + m_cameraData.m_extraTopRatio) * 0.5f));
       
        //Vector3 constraintDiff = temp.max - temp.min;
        Vector3 constraintDiff = maxBounds.max - maxBounds.min;
        m_testBounds = maxBounds;
        m_maxConstraintSize = Mathf.Min(Mathf.Abs(constraintDiff.y), Mathf.Abs(constraintDiff.x * m_heightToWidthRatio)) * 0.5f;
        Debug.Log("ratio is " 
        + m_heightToWidthRatio);
    }
    void LateUpdate(){
        Vector3 constraintDiff = m_testBounds.max - m_testBounds.min;
        m_maxConstraintSize = Mathf.Min(Mathf.Abs(constraintDiff.y), Mathf.Abs(constraintDiff.x * m_heightToWidthRatio)) * 0.5f;
        CameraBound tempBound = GetCenterPointAndZoom();

        float newZoom = Mathf.Clamp(tempBound.m_zoomSize, m_cameraData.m_minZoom, Mathf.Min(m_maxConstraintSize,m_cameraData.m_maxZoom));//Mathf.Lerp(m_minZoom, m_maxZoom, tempBound.m_zoomSize / m_maxDistance);
        m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, newZoom, Time.deltaTime * m_lerpScale);
        Vector3 targetPosition = tempBound.m_center;// + m_offset * m_camera.orthographicSize / m_minZoom;

        targetPosition = new Vector3 (
            Mathf.Clamp (targetPosition.x, 
                        m_testBounds.min.x + m_camera.orthographicSize / m_heightToWidthRatio, 
                        m_testBounds.max.x - m_camera.orthographicSize / m_heightToWidthRatio), 
            Mathf.Clamp (targetPosition.y, 
                        m_testBounds.min.y + m_camera.orthographicSize
                        , m_testBounds.max.y - m_camera.orthographicSize));
        targetPosition.z = -10;
        transform.position = targetPosition;// Vector3.SmoothDamp(transform.position, targetPosition, ref m_cameraVelocity, m_smoothTime);

    }

    private CameraBound GetCenterPointAndZoom(){
        CameraBound newBound = new CameraBound();
        var bounds = new Bounds(m_targets[0].position, Vector3.zero);
        for (int i = 0; i < m_targets.Count; i++){
            bounds.Encapsulate(m_targets[i].position);
        }
        
        newBound.m_center = bounds.center;
       
        newBound.m_zoomSize = Mathf.Max(bounds.size.x * m_heightToWidthRatio, 
        bounds.size.y + m_cameraData.m_extraHeight) * 0.5f;

        newBound.m_zoomSize += m_cameraData.m_margin;// * Mathf.Max(1,newBound.m_zoomSize / m_cameraData.m_minZoom);

        return newBound;
    }
    // public void RemoveInactive( signal){
    //     if (m_targets.Contains(signal.transform)) m_targets.Remove(signal.transform);
    // }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_testBounds.center, m_testBounds.size);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube( m_cameraData.m_cameraConstraints.center,  m_cameraData.m_cameraConstraints.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(m_tempBounds.center, m_tempBounds.size);
    }
}
