using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPoint{
    public Vector2 pos;
    public float g;
    public float h;
    public float f;

    //Constructors
    public AStarPoint(){
        pos = Vector2.zero;
        g = 0f;
        h = 0f;
        f = 0f;
    }

    public AStarPoint(Vector2 p_, float g_ = 0f, float h_ = 0f, float f_ = 0f){
        pos = p_;
        g = g_;
        h = h_;
        f = f_;
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
}

public class AStar : MonoBehaviour
{
    public float gridSize = 1f;
    public Stack<AStarPoint> open;
    public Stack<AStarPoint> closed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PathFind(Vector2 target){
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
            foreach (AStarPoint child in GetValidNeighbors(cur)){

            }
        }
        

    }

    //Gets the neighbors of a point
    public List<AStarPoint> GetValidNeighbors(AStarPoint p){
        List<AStarPoint> neighbors = GetNeighbors(p);
        return neighbors;
    }

    public List<AStarPoint> GetNeighbors(AStarPoint p){
        List<AStarPoint> neighbors = new List<AStarPoint>();
        return neighbors;
    }

}
