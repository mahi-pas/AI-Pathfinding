using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AStar
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
    public AStar(){
        gridSize = 1f;
        collisionLayer = Physics.DefaultRaycastLayers;
        open = new List<AStarPoint>();
        closed = new List<AStarPoint>();
        visualizers = new List<GameObject>();
    }

    public AStar(float gridSize_,LayerMask collisionLayer_){
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

        open.Add(new AStarPoint(position));
        Vector3 converted = position;
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
                if(child.pos == target){
                    //ClearVisualizers();
                    return child;
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

    //Gets the neighbors of a point
    public List<AStarPoint> GetValidNeighbors(AStarPoint p){
        List<AStarPoint> neighbors = new List<AStarPoint>();
        foreach(AStarPoint a in GetNeighbors(p)){
            if(!HasCollision(a)){
                neighbors.Add(a);
            }
        }
        return neighbors;
    }

    public bool HasCollision(AStarPoint p){
        return Physics2D.OverlapCircle(p.pos, (gridSize*0.9f)/2, collisionLayer) != null;
    }

    public List<AStarPoint> GetNeighbors(AStarPoint p){
        List<AStarPoint> neighbors = new List<AStarPoint>();
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(0,1) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(0,-1) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(1,1) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(1,-1) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(-1,1) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(-1,-1) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(1,0) * gridSize), p));
        neighbors.Add(new AStarPoint(p.pos + (new Vector2(-1,0) * gridSize), p));
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
