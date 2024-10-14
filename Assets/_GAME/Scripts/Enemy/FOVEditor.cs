using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyBrain))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyBrain enemyBrain = (EnemyBrain)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyBrain.transform.position, Vector3.up, Vector3.forward, 360, enemyBrain.Radius);

        Vector3 viewAngleA = GetVectorFromAngle(enemyBrain.transform.eulerAngles.y, -enemyBrain.Angle / 2);
        Vector3 viewAngleB = GetVectorFromAngle(enemyBrain.transform.eulerAngles.y, enemyBrain.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemyBrain.transform.position, enemyBrain.transform.position + viewAngleA * enemyBrain.Radius);
        Handles.DrawLine(enemyBrain.transform.position, enemyBrain.transform.position + viewAngleB * enemyBrain.Radius);
        
    }

    private Vector3 GetVectorFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


}
