using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MeshSaver : MonoBehaviour
{
    public static string s_filePath = "D:/UNI/Y3/#CTEC3451/Final Year Project/Unity/Final Year Project/Assets/SavedModels/";
    public static bool SaveToOBJ(Mesh mesh) {
        return SaveToOBJ("Object", mesh);
    }

    public static bool SaveToOBJ(string name, Mesh mesh) {
        if(mesh == null) 
        {
            Debug.LogError("Could not save mesh, no mesh given!");
            return false;
        }

        if(mesh.vertices.Length <= 0)
        {
            Debug.LogWarning("Could not save mesh, mesh has no vertices!");
            return false;
        }

        int attribs = 0;
        string fileOutput = "# This file was exported by MeshSaver, written By Joshua Simons\n\n";

        string verts = ToVerts(mesh.vertices);
        if(verts != "") attribs += 1;

        string uvs = ToUVs(mesh.uv);
        if(uvs != "") attribs += 1;

        string norms = ToNorms(mesh.normals);
        if(norms != "") attribs += 1;

        fileOutput += "g default\n";
        fileOutput += verts;
        fileOutput += uvs;
        fileOutput += norms;
        fileOutput += "s off\n";
        fileOutput += "g " + name + "\n";
        fileOutput += ToFaces(mesh.triangles, attribs);

        Save(name, fileOutput);

        return true;
    }

    public static void Save(string name, string data)
    {
        string filePath; 
        if(s_filePath == "") filePath = Application.persistentDataPath + name + ".obj";
        else filePath = s_filePath + name + ".obj";

        if(File.Exists(filePath)) Debug.Log("Overwriting: " + filePath);
        else Debug.Log("Writing new file: " + filePath);
        
        File.WriteAllText(filePath, data);
    }

    public static string ToVerts(Vector3[] vertices) {
        string value = "";
        foreach(Vector3 vec in vertices)
        {
            value += "v ";
            value += vec.x.ToString("F6") + " ";
            value += vec.y.ToString("F6") + " ";
            value += vec.z.ToString("F6") + " ";
            value += "\n";
        }
        return value;
    }

    public static string ToUVs(Vector2[] uvs) {
        string value = "";
        foreach(Vector2 vec in uvs)
        {
            value += "vt ";
            value += vec.x.ToString("F6") + " ";
            value += vec.y.ToString("F6") + " ";
            value += "\n";
        }
        return value;
    }
    
    public static string ToNorms(Vector3[] normals) {
        string value = "";
        foreach(Vector3 vec in normals)
        {
            value += "vn ";
            value += vec.x.ToString("F6") + " ";
            value += vec.y.ToString("F6") + " ";
            value += vec.z.ToString("F6") + " ";
            value += "\n";
        }
        return value;
    }

    public static string ToFaces(int[] triangles, int attribs) {
        string value = "";
        for(int i = 0; i < (triangles.Length / 3); i++)
        {
            int index = i * 3;
            value += "f ";
            value += (triangles[index] + 1).ToString("D");
            for(int j = 1; j < attribs; j++) value += "/" + (triangles[index] + 1).ToString("D");
            value += " ";
            value += (triangles[index + 1] + 1).ToString("D");
            for(int j = 1; j < attribs; j++) value += "/" + (triangles[index + 1] + 1).ToString("D");
            value += " ";
            value += (triangles[index + 2] + 1).ToString("D");
            for(int j = 1; j < attribs; j++) value += "/" + (triangles[index + 2] + 1).ToString("D");
            value += "\n";
        }
        return value;
    }
}