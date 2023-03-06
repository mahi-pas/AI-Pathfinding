using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private List<GameObject> linkedNodes = new List<GameObject>(), edges = new List<GameObject>();

    private void Start()
    {
        GetLinkedNodesAndEdges();
        //Debug.Log(gameObject.name + " " + linkedNodes.Count + " " + edges.Count);
    }

    private void GetLinkedNodesAndEdges()
    {
        GameObject edgesParent = GameObject.Find("Edges");
        string waypointNumber = gameObject.name.Substring(8);
        for (int i = 0; i < edgesParent.transform.childCount; i++)
        {
            GameObject edge = edgesParent.transform.GetChild(i).gameObject;
            if (edge.name.Contains("_" + waypointNumber + "_"))
            {
                edges.Add(edge);
                linkedNodes.Add(GetOtherNodeInEdge(edge));
            }
        }
    }

    // Helper method that determines the node connected to an edge that isn't this node
    private GameObject GetOtherNodeInEdge(GameObject edge)
    {
        // First remove anything before and including the first _
        string edgeName = edge.name;
        edgeName = edgeName.Substring(5);

        // Then check what comes after the _ and see if that is the other node
        string thisNodeNumber = gameObject.name.Substring(8);
        string nodeNumber = "";
        while (true)
        {
            nodeNumber += edgeName.Substring(0, 1);
            edgeName = edgeName.Substring(1);
            if (edgeName.Substring(0, 1) == "_")
            {
                edgeName = edgeName.Substring(1);
                break;
            }
        }
        
        if (nodeNumber != thisNodeNumber)
        {
            return GameObject.Find("Waypoint" + nodeNumber);
        }

        // If not, then take what comes after the second _
        nodeNumber = "";
        while (true)
        {
            nodeNumber += edgeName.Substring(0, 1);
            edgeName = edgeName.Substring(1);
            if (edgeName.Substring(0, 1) == "_")
            {
                break;
            }
        }
        return GameObject.Find("Waypoint" + nodeNumber);
    }

    public GameObject GetLinkedNode(int index)
    {
        return linkedNodes[index];
    }

    public GameObject GetEdge(int index)
    {
        return edges[index];
    }

    public GameObject GetEdge(GameObject waypoint)
    {
        for (int i = 0; i < edges.Count; i++)
        {
            if (GetLinkedNode(i) == waypoint)
            {
                return GetEdge(i);
            }
        }
        return null;
    }

    public int GetNumEdges()
    {
        return edges.Count;
    }
}
