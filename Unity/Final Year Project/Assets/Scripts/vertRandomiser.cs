using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertRandomiser : MonoBehaviour
{
    public GameObject m_vertPrefab;
    public Vector2 m_Xcaps;
    public Vector2 m_Ycaps;
    public Vector2 m_Zcaps;

    public List<meshVert> m_verts = new List<meshVert>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewVert()
    {
        GameObject go = Instantiate(m_vertPrefab, transform.position + new Vector3(Random.Range(m_Xcaps.x, m_Xcaps.y), Random.Range(m_Ycaps.x, m_Ycaps.y), Random.Range(m_Zcaps.x, m_Zcaps.y)), Quaternion.identity);
        go.transform.parent = transform;

        int[] closest = new int[4];
        closest[0] = 0;
        closest[1] = 1;
        closest[2] = 2;
        closest[3] = 3;
        List<float> distances = new List<float>();

        if(m_verts.Count > 8)
        {
            //Go through every vertex
            for(int i = 0; i < m_verts.Count - 1; i++)
            {
                //add it to the distances
                distances.Add(Vector3.Distance(go.transform.position, m_verts[i].transform.position));

                //Setup variables before there's enough stuff in the lsit
                if(i < 4) continue;

                //for everything in closest
                for(int j = 0; j < 4; j++)
                {
                    //check if its closer
                    if(distances[i] < distances[closest[j]])
                    {
                        //if it is move everything back 1 place
                        for(int k = 1 - j; k > 0; k--)
                        {
                            closest[k] = closest[k-1];
                        }
                        closest[j] = i;
                        break;
                    }
                }
            }

            go.GetComponent<meshVert>().ConnectVertex(m_verts[closest[0]]);
            go.GetComponent<meshVert>().ConnectVertex(m_verts[closest[1]]);
            go.GetComponent<meshVert>().ConnectVertex(m_verts[closest[2]]);
            go.GetComponent<meshVert>().ConnectVertex(m_verts[closest[3]]);
            m_verts[closest[0]].ConnectVertex(go.GetComponent<meshVert>());
            m_verts[closest[1]].ConnectVertex(go.GetComponent<meshVert>());
            m_verts[closest[2]].ConnectVertex(go.GetComponent<meshVert>());
            m_verts[closest[3]].ConnectVertex(go.GetComponent<meshVert>());
        }
        
        m_verts.Add(go.GetComponent<meshVert>());
    }
}
 