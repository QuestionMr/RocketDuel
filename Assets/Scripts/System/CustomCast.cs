using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCast
{
    public static int OverlapCone(Vector2 origin, float radius, Vector2 direction, float angleOffset, ContactFilter2D collideLayers, List<Collider2D> results, float duration = -1){
        direction = direction.normalized * radius;
        Vector2 upperVec = Quaternion.AngleAxis(angleOffset, Vector3.forward) * direction;
        Vector2 lowerVec = Quaternion.AngleAxis(-angleOffset, Vector3.forward) * direction;

        Physics2D.OverlapCircle(origin, radius, collideLayers, results);
        for (int i = 0; i < results.Count; i++){
            Collider2D coll = results[i];
            Vector2 diff = (Vector2)coll.transform.position - origin;
            if (Vector2.Angle(diff, direction) > angleOffset) {
                //Debug.Log("Remove " + coll);
                results.Remove(coll);
                i--;
            }   
        }
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        Physics2D.Raycast(origin, upperVec, collideLayers, hits, radius);
        foreach (RaycastHit2D hit in hits){
            if (!results.Contains(hit.collider)) results.Add(hit.collider);
        
        }
        Physics2D.Raycast(origin, lowerVec, collideLayers, hits, radius);
        foreach (RaycastHit2D hit in hits){
            if (!results.Contains(hit.collider)) results.Add(hit.collider);
        }

        if (duration >= 0){
            // Debug.DrawLine(origin, origin + upperVec,Color.red, duration);
            // Debug.DrawLine(origin, origin + lowerVec,Color.red, duration);
            // Debug.DrawLine(origin, origin + direction,Color.blue, duration);
            foreach (Collider2D coll in results){
                //Debug.DrawLine(origin, coll.transform.position, Color.green, duration);
            }
        }
        return results.Count;
    }

    public static int FanCast(Vector2 origin, float radius, Vector2 direction, 
                            float angleOffset, ContactFilter2D collideLayers, ContactFilter2D blockingLayers,
                            List<RaycastHit2D> results, int numOfRay = 2,
                            float duration = 0){

        results.Clear();       
        direction = direction.normalized * radius;
        numOfRay = Mathf.Max(numOfRay, 2);
        float rayAngleOffset = angleOffset * 2 / (numOfRay - 1);
        float startingAngle = angleOffset;
        Vector2 currentRay;
        LayerMask lm = collideLayers.layerMask | blockingLayers.layerMask;
        for (int i = 1; i <= numOfRay; i++){
            currentRay = Quaternion.AngleAxis(startingAngle, Vector3.forward) * direction;
            //Debug.DrawLine(origin, origin + currentRay, Color.red, duration);
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, currentRay, radius, lm);
            foreach (RaycastHit2D hit in hits){
                if (((1 << hit.collider.gameObject.layer) & blockingLayers.layerMask) != 0) break;
                results.Add(hit);
            }
            startingAngle -= rayAngleOffset;
        }
        if (duration >= 0){
            foreach (RaycastHit2D hit in results) {
                //Debug.DrawRay(origin, (Vector2)hit.collider.transform.position - origin, Color.green, duration);
                //Debug.DrawLine(origin, hit.point, Color.green, duration);
                //Debug.Log(hit.collider);    
            }
        }
        return results.Count;
    }
}
