using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("Agent")]
    public float speed = 5f;
    [Header("A*")]
    public AStar astar;
    public float gridSize;
    public LayerMask collisionLayer;
    public List<Vector2> target;

    [Header("Debug // Temporary")]
    public Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        astar = new AStar(gridSize, collisionLayer);
        //Test();
        PathFindAStar(new Vector2(transform.position.x, transform.position.y), targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    }

    public void MoveAlongPath(){
        if(target.Count == 0) return;
        if(new Vector2(transform.position.x, transform.position.y) != target[target.Count-1]){
            transform.position = Vector2.MoveTowards(transform.position, target[target.Count-1], speed * Time.deltaTime);
        }
        else{
            if(target.Count != 0) target.RemoveAt(target.Count-1);
        }
        
    }

    public void PathFindAStar(Vector2 pos, Vector2 targ){
        AStarPoint path = astar.PathFind(pos, targ);
        target = new List<Vector2>();
        while(path != null){
            target.Add(path.pos);
            path = path.prev;
        }
        Debug.Log(string.Join(", ", target));
    }

    //Just some tester code
    public void Test(){
        AStarPoint p = new AStarPoint();
        Debug.Log(p+ " Has Collision = " + astar.HasCollision(p));
        AStarPoint q = new AStarPoint(new Vector2(0,-1));
        Debug.Log(q + " Has Collision = " + astar.HasCollision(q));
        foreach (AStarPoint child in astar.GetValidNeighbors(p)){
            Debug.Log(child + " Has Collision = " + astar.HasCollision(child));
        }
    }
}
