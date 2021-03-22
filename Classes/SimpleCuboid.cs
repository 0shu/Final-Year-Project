using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleCuboid))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleCuboid myTarget = (SimpleCuboid)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Apply to V"))
        {
            myTarget.CallVFromInspector();
        }

        if (GUILayout.Button("Apply to X"))
        {
            myTarget.CallXFromInspector();
        }

        if (GUILayout.Button("Apply to Y"))
        {
            myTarget.CallYFromInspector();
        }

        if (GUILayout.Button("Apply to Z"))
        {
            myTarget.CallZFromInspector();
        }

        if (GUILayout.Button("Reset"))
        {
            myTarget.Reset();
        }
    }
}

public class SimpleCuboid : MonoBehaviour
{
    public enum Axis {X, Y, Z};

    public float V = 1;
    public float X = 1;
    public float Y = 1;
    public float Z = 1;

    [Range(0.0f, 2.0f)]
    public float Scalar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) RaycastMouseHit();
    }

    public void CallVFromInspector()
    {
        float input = V * Scalar;
        RecalculateV(input);
    }

    public void CallXFromInspector()
    {
        float input = X * Scalar;
        RecalculateX(input);
    }

    public void CallYFromInspector()
    {
        float input = Y * Scalar;
        RecalculateY(input);
    }

    public void CallZFromInspector()
    {
        float input = Z * Scalar;
        RecalculateZ(input);
    }

    void RecalculateV(float newV)
    {
        float factor = newV / V;
        factor = Mathf.Pow(factor, 1f / 3f);
        V = newV;
        X *= factor;
        Y *= factor;
        Z *= factor;

        ReScale();
    }

    void RecalculateX(float newX)
    {
        float A = Y / Z;
        Z = Mathf.Pow(V / (newX * A), 0.5f);
        Y = Z * A;
        X = newX;

        ReScale();
    }

    void RecalculateY(float newY)
    {
        float A = X / Z;
        Z = Mathf.Pow(V / (newY * A), 0.5f);
        X = Z * A;
        Y = newY;

        ReScale();
    }

    void RecalculateZ(float newZ)
    {
        float A = X / Y;
        Y = Mathf.Pow(V / (newZ * A), 0.5f);
        X = Y * A;
        Z = newZ;

        ReScale();
    }

    void ReScale ()
    {
        transform.localScale = new Vector3(X, Y, Z);
    }

    public void Reset()
    {
        X = 1;
        Y = 1;
        Z = 1;
        V = 1;
        ReScale();
    }

    void RaycastMouseHit()
    {
        Debug.Log("Starting raycast!");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
            Debug.Log("Hit Detected! " + hit.point.x + " " + hit.point.y + " " + hit.point.z);

            Vector3 abs = hit.point - hit.collider.gameObject.transform.position;
            abs = new Vector3(abs.x / X, abs.y / Y, abs.z / Z);
            abs = Vector3.Normalize(abs);
            abs = new Vector3(Mathf.Abs(abs.x), Mathf.Abs(abs.y), Mathf.Abs(abs.z));

            if (abs.x > abs.y && abs.x > abs.z) CallXFromInspector();
            if (abs.y > abs.x && abs.y > abs.z) CallYFromInspector();
            if (abs.z > abs.y && abs.z > abs.x) CallZFromInspector();
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
        }
    }
}
