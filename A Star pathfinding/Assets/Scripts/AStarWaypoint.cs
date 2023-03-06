using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AStarWaypoint
{
    public float gridSize;
    public List<AStarPoint> open;
    public List<AStarPoint> closed;
    public LayerMask collisionLayer;
    public GameObject visualizer;
    public List<GameObject> visualizers;
    public MonoBehaviour mb;

    public enum Heuristic {Euclidean, Manhattan};
    public Heuristic heuristicMode = Heuristic.Euclidean;
    public float gWeight = 1f;
    public float hWeight = 1f;

    //Constructors
    public AStarWaypoint(){
        gridSize = 1f;
        collisionLayer = Physics.DefaultRaycastLayers;
        open = new List<AStarPoint>();
        closed = new List<AStarPoint>();
        visualizers = new List<GameObject>();
    }

    public AStarWaypoint(float gridSize_,LayerMask collisionLayer_){
        gridSize = gridSize_;
        collisionLayer = collisionLayer_;
        open = new List<AStarPoint>();
        closed = new List<AStarPoint>();
        visualizers = new List<GameObject>();
    }

    public AStarPoint PathFind(Vector2 position, Vector2 target){
        open = new List<AStarPoint>();
        closed = new List<AStarPoint>();
        visualizers = new List<GameObject>();

        GameObject startNode = FindClosestNode(position);
        GameObject endNode = FindClosestNode(target);

        open.Add(new AStarPoint(startNode, null));
        Vector3 converted = startNode.transform.position;
        AddVisualizer(converted);

        while(open.Count != 0){
            int lowestFIndex = AStarPoint.LowestFValueIndex(open);
            AStarPoint cur = open[lowestFIndex];
            open.RemoveAt(lowestFIndex);
            //check for other paths
            foreach (AStarPoint child in GetValidNeighbors(cur)){
                //visualize
                Vector3 converted2 = child.pos;
                AddVisualizer(converted2);
                //check if reached target position
                if(child.waypoint == endNode){
                    //ClearVisualizers();
                    AStarPoint finalPos = new AStarPoint(target, child);
                    return finalPos;
                }
                switch (heuristicMode){
                    case Heuristic.Manhattan:
                        child.g = cur.g + ManhattanDistance(cur.pos,child.pos);
                        child.h = ManhattanDistance(child.pos, target);
                        break;
                    default: //Also for Euclidean
                        child.g = cur.g + Vector2.Distance(cur.pos,child.pos);
                        child.h = Vector2.Distance(child.pos, target);
                        break;
                }
                
                child.f = (child.g * gWeight) + (child.h * hWeight);

                bool hasLowestF = true;
                //if lower f on open
                foreach (AStarPoint point in open){
                    if(point.pos == child.pos && child.f >= point.f) hasLowestF = false;
                }
                //if lower f on closed
                foreach (AStarPoint point in closed){
                    if(point.pos == child.pos && child.f >= point.f) hasLowestF = false;
                }
                if(hasLowestF) {
                    open.Add(child);
                }
                
            }
            //add current to closed list
            closed.Add(cur);
        }
        //ClearVisualizers();
        return null;

    }

    // Find the node closest to a target position
    public GameObject FindClosestNode(Vector2 pos)
    {
        GameObject p = GameObject.Find("Waypoints");
        GameObject closest = p.transform.GetChild(0).gameObject;
        for (int i = 1; i < p.transform.childCount; i++)
        {
            if (Vector2.Distance(pos, p.transform.GetChild(i).position) < Vector2.Distance(pos, closest.transform.position))
            {
                closest = p.transform.GetChild(i).gameObject;
            }
        }
        return closest;
    }

    //Gets the neighbors of a point
    public List<AStarPoint> GetValidNeighbors(AStarPoint p){
        List<AStarPoint> neighbors = new List<AStarPoint>();
        Waypoint waypoint = p.waypoint.GetComponent<Waypoint>();
        for (int i = 0; i < waypoint.GetNumEdges(); i++)
        {
            neighbors.Add(new AStarPoint(waypoint.GetLinkedNode(i), p));
        }
        return neighbors;
    }

    //Heuristics
    public float ManhattanDistance(Vector2 x, Vector2 y){
        return (Mathf.Abs(x.x - y.x) + Mathf.Abs(x.y - y.y));
    }
    
    //Visualization
    public void AddVisualizer(Vector3 position){
        visualizers.Add(GameObject.Instantiate(visualizer, position, Quaternion.identity));
    }

    public void ClearVisualizers(){
        for(int i = visualizers.Count-1; i>=0; i--){
            GameObject.Destroy(visualizers[i]);
        }
    }

}
