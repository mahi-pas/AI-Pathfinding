using System.Collections;
using System.Collections.Generic;
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

    public AStarPoint(Vector2 p_, AStarPoint prev_){
        pos = p_;
        g = 0f;
        h = 0f;
        f = 0f;
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
        if(prev != null) return "("+pos.x+","+pos.y+") g: "+g+" h:"+h+" f:"+f + " prev: "+ prev;
        else return "("+pos.x+","+pos.y+") g: "+g+" h:"+h+" f:"+f;
    }

    public static int LowestFValueIndex(List<AStarPoint> points){
        float lowestF = float.MaxValue;
        int lowestIndex = 0;
        for(int i = 0; i<points.Count; i++){
            if(points[i].f < lowestF){
                lowestF = points[i].f;
                lowestIndex = i;
            }
        }
        return lowestIndex;
    }

}