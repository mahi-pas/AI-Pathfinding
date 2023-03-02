using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AStarPoint{
    public Vector2 pos;
    public float g;
    public float h;
    public float f;
    public AStarPoint prev;

    //Constructors
    public AStarPoint(){
        pos = Vector2.zero;
        g = 0f;
        h = 0f;
        f = 0f;
        prev = null;
    }

    public AStarPoint(Vector2 p_, float g_ = 0f, float h_ = 0f, float f_ = 0f, AStarPoint prev_ = null){
        pos = p_;
        g = g_;
        h = h_;
        f = f_;
        prev = prev_;
    }

    //Functions
    public override bool Equals(object obj){
        if(obj == null || !this.GetType().Equals(obj.GetType())){
            return false;
        }
        else{
            return pos == ((AStarPoint)obj).pos;
        }
    }

    public override int GetHashCode(){
        return 0;
    }

    public override string ToString()
    {
        return "("+pos.x+","+pos.y+") g: "+g+" h:"+h+" f:"+f;
    }

}

public class AStar
{
    public float gridSize;
    public Stack<AStarPoint> open;
    public Stack<AStarPoint> closed;
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

    public void PathFind(Vector2 target, Vector2 position){
        open = new Stack<AStarPoint>();
        closed = new Stack<AStarPoint>();

        open.Push(new AStarPoint());

        while(open.Count != 0){
            AStarPoint cur = open.Pop();
            //check if reached target position
            if(cur.pos == target){
                Debug.Log("Reached Target!");
                return;
            }
            //add current to closed list
            closed.Push(cur);
            //check for other paths
            foreach (AStarPoint child in GetValidNeighbors(cur, position)){

            }
        }
        

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

    public List<AStarPoint> GetNeighbors(AStarPoint p){
        List<AStarPoint> neighbors = new List<AStarPoint>();
        return neighbors;
    }

    public bool HasCollision(AStarPoint p, Vector2 position){
        return Physics2D.OverlapCircle(position + p.pos, (gridSize*0.9f)/2, collisionLayer) != null;
    }

}
