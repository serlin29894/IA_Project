using UnityEngine;
using System.Collections;

public class CamaraFollow : MonoBehaviour {

    public GameObject Target;
    private Vector3 Position;


	// Use this for initialization
	void Start ()
    {
        Position.x = Target.transform.position.x;
        Position.y = Target.transform.position.y;
        Position.z = -10;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Position.x = Target.transform.position.x;
        Position.y = Target.transform.position.y;
      
        this.transform.position = Position;
	
	}
}
