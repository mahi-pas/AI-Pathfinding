using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Agent agent;
    public enum Mode {None, SetTarget, PlaceBlock};
    public Mode mode = Mode.None;
    public Text instructions;

    public GameObject placedWall;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && mode == Mode.SetTarget){
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y));
            Debug.Log(mousePosition);

            if(!agent.astar.HasCollision(new AStarPoint(mousePosition))){
                Vector2 agentPosition = new Vector2(Mathf.Round(agent.transform.position.x), Mathf.Round(agent.transform.position.y));
                agent.PathFindAStar(agentPosition, mousePosition);

                //UI stuff
                instructions.text = "";
                mode = Mode.None;
            }
        }
        if(Input.GetMouseButtonDown(0) && mode == Mode.PlaceBlock){
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y));
            Debug.Log(mousePosition);
            Vector2 agentPosition = new Vector2(Mathf.Round(agent.transform.position.x), Mathf.Round(agent.transform.position.y));
            if(!agent.astar.HasCollision(new AStarPoint(mousePosition)) && mousePosition != agentPosition){
                Instantiate(placedWall, mousePosition, Quaternion.identity);
                //recalculate agent movement
                if(agent.target.Count>0)
                    agent.PathFindAStar(agentPosition, agent.prevTarget);
                //UI stuff
                instructions.text = "";
                mode = Mode.None;
            }
        }
    }

    public void SetMode(int m){
        mode = (Mode) m;
    }

    public void SetInstructions(string s){
        instructions.text = s;
    }
}
