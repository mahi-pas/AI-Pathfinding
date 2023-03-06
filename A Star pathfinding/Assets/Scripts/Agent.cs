using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("Agent")]
    public float speed = 5f;
    public enum PathfindMode {AStar, Waypoint};
    public PathfindMode pathfindMode = PathfindMode.AStar;
    [Header("A*")]
    public AStar astar;
    public AStarWaypoint astarwaypoint;
    public float gridSize;
    public LayerMask collisionLayer;
    public List<Vector2> target;
    public GameObject visualizer;
    public GameObject pathVisualizer;
    public List<GameObject> pathVisualizers;
    public Vector2 prevTarget;

    // Start is called before the first frame update
    void Start()
    {
        astar = new AStar(gridSize, collisionLayer);
        astar.visualizer = visualizer;
        astarwaypoint = new AStarWaypoint(gridSize, collisionLayer);
        astarwaypoint.visualizer = visualizer;

        prevTarget = transform.position;
        //Test();
        //PathFindAStar(new Vector2(transform.position.x, transform.position.y), targetPos);
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

    public void PathFind(Vector2 pos, Vector2 targ){
        switch (pathfindMode){
            case PathfindMode.AStar:
                PathFindAStar(pos, targ);
                break;
            case PathfindMode.Waypoint:
                PathFindAStarWaypoint(pos, targ);
                break;
        }

    }

    public void PathFindAStar(Vector2 pos, Vector2 targ){
        prevTarget = targ;
        astar.ClearVisualizers();
        astarwaypoint.ClearVisualizers();
        AStarPoint path = astar.PathFind(pos, targ);
        target = new List<Vector2>();
        while(path != null){
            target.Add(path.pos);
            path = path.prev;
        }
        ShowPath(target);
        Debug.Log(string.Join(", ", target));
    }

    public void PathFindAStarWaypoint(Vector2 pos, Vector2 targ)
    {
        prevTarget = targ;
        astar.ClearVisualizers();
        astarwaypoint.ClearVisualizers();
        AStarPoint path = astarwaypoint.PathFind(pos, targ);
        GameObject.Find("EndEdge").GetComponent<LineRenderer>().SetPosition(0, path.pos);
        GameObject.Find("EndEdge").GetComponent<LineRenderer>().SetPosition(1, path.prev.pos);
        target = new List<Vector2>();
        while (path != null)
        {
            target.Add(path.pos);
            if (path.prev != null && path.prev.prev == null)
            {
                GameObject.Find("StartEdge").GetComponent<LineRenderer>().SetPosition(0, path.pos);
                GameObject.Find("StartEdge").GetComponent<LineRenderer>().SetPosition(1, path.prev.pos);
            }
            path = path.prev;
            if (path != null && path.waypoint && path.prev != null)
            {
                GameObject e = path.waypoint.GetComponent<Waypoint>().GetEdge(path.prev.waypoint);
                if (e)
                {
                    e.GetComponent<LineRenderer>().startColor = Color.green;
                    e.GetComponent<LineRenderer>().endColor = Color.green;
                }
            }
        }
        ShowPath(target);
        Debug.Log(string.Join(", ", target));
    }

    //Visualization
    public void ShowPath(List<Vector2> path){
        ClearPath();
        pathVisualizers = new List<GameObject>();
        foreach(Vector2 pos in path){
            pathVisualizers.Add(Instantiate(pathVisualizer, pos, Quaternion.identity));
        }
    }

    public void ClearPath(){
        for(int i = pathVisualizers.Count-1; i>=0; i--){
            GameObject.Destroy(pathVisualizers[i]);
        }
    }

    //Control Algorithm
    public void ChangeHeuristicMode(int mode){
        switch(mode){
            case 0:
                astar.heuristicMode = AStar.Heuristic.Euclidean;
                break;
            case 1:
                astar.heuristicMode = AStar.Heuristic.Manhattan;
                break;
        }
    }

    public void SetPathfindMode(int m){
        pathfindMode = (PathfindMode) m;
    }

    public void ChangeGWeight(string weight){
        astar.gWeight = float.Parse(weight);
    }

    public void ChangeHWeight(string weight){
        astar.gWeight = float.Parse(weight);
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
