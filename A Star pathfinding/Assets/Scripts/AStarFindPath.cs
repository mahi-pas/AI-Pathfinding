using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapLocation       
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x, z);
    }

    public static MapLocation operator +(MapLocation a, MapLocation b)
       => new MapLocation(a.x + b.x, a.z + b.z);

    public override bool Equals(object obj){
        if(obj == null || !this.GetType().Equals(obj.GetType())){
            return false;
        }
        else{
            return x == ((MapLocation)obj).x && z == ((MapLocation)obj).z;
        }
    }

    public override int GetHashCode(){
        return 0;
    }

}

public class PathMarker{
    public MapLocation loc;
    public float g;
    public float h;
    public float f;
    public GameObject marker;
    public PathMarker parent;

    public PathMarker(MapLocation l, float g_, float h_, float f_, GameObject m, PathMarker p){
        loc = l;
        g = g_;
        h = h_;
        f = f_;
        marker = m;
        parent = p;
    }

    public override bool Equals(object obj){
        if(obj == null || !this.GetType().Equals(obj.GetType())){
            return false;
        }
        else{
            return loc.Equals(((PathMarker) obj).loc);
        }
    }

    public override int GetHashCode(){
        return 0;
    }

}

public class AStarFindPath : MonoBehaviour
{

    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
