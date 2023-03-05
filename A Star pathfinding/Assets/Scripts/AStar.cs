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

    //Constructors
    public AStar(){
        gridSize = 1f;
        collisionLayer = Physics.DefaultRaycastLayers;
    }

    public AStar(float gridSize_,LayerMask collisionLayer_){
        gridSize = gridSize_;
        collisionLayer = collisionLayer_;
    }

    public AStarPoint PathFind(Vector2 position, Vector2 target){
        open = new List<AStarPoint>();
        closed = new List<AStarPoint>();

        open.Add(new AStarPoint());

        while(open.Count != 0){
            int lowestFIndex = AStarPoint.LowestFValueIndex(open);
            AStarPoint cur = open[lowestFIndex];
            open.RemoveAt(lowestFIndex);
            //check for other paths
            foreach (AStarPoint child in GetValidNeighbors(cur, position)){
                //check if reached target position
                if(child.pos == target){
                    return child;
                }
                child.g = cur.g + Vector2.Distance(cur.pos,child.pos);
                child.h = Vector2.Distance(child.pos, target);
                child.f = child.g + child.h;

                bool hasLowestF = true;
                //if lower f on open
                foreach (AStarPoint point in open){
                    if(point.pos == child.pos && child.f >= point.f) hasLowestF = false;
                }
                //if lower f on closed
                foreach (AStarPoint point in closed){
                    if(point.pos == child.pos && child.f >= point.f) hasLowestF = false;
                }
                if(hasLowestF) open.Add(child);
                
            }
            //add current to closed list
            closed.Add(cur);
        }
        return null;

    }

    //Gets the neighbors of a point
    public List<AStarPoint> GetValidNeighbors(AStarPoint p, Vector2 position){
        List<AStarPoint> neighbors = new List<AStarPoint>();
        foreach(AStarPoint a in GetNeighbors(p)){
            if(!HasCollision(a, position)){
                neighbors.Add(a);
            }
        }
        return neighbors;
    }

    public bool HasCollision(AStarPoint p, Vector2 position){
        return Physics2D.OverlapCircle(position + p.pos, (gridSize*0.9f)/2, collisionLayer) != null;
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



}
