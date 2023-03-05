using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Agent agent;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y));
            Debug.Log(mousePosition);

            if(!agent.astar.HasCollision(new AStarPoint(mousePosition))){
                Vector2 agentPosition = new Vector2(Mathf.Round(agent.transform.position.x), Mathf.Round(agent.transform.position.y));
                agent.PathFindAStar(agentPosition, mousePosition);
            }
        }
    }
}
