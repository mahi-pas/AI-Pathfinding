using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    public AStar astar;
    public float gridSize;
    public LayerMask collisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        astar = new AStar(gridSize, collisionLayer);
        //Test();
        PathFindAStar(new Vector2(0,0), new Vector2(1,3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PathFindAStar(Vector2 pos, Vector2 target){
        AStarPoint path = astar.PathFind(pos, target);
        Debug.Log(path);
    }

    //Just some tester code
    public void Test(){
        AStarPoint p = new AStarPoint();
        Debug.Log(p+ " Has Collision = " + astar.HasCollision(p,transform.position));
        AStarPoint q = new AStarPoint(new Vector2(0,-1));
        Debug.Log(q + " Has Collision = " + astar.HasCollision(q,transform.position));
        foreach (AStarPoint child in astar.GetValidNeighbors(p, transform.position)){
            Debug.Log(child + " Has Collision = " + astar.HasCollision(child,transform.position));
        }
    }
}
