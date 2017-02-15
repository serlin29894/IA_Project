using UnityEngine;
using System.Collections;

public class Seeker_Blackboard : MonoBehaviour {

    public float WanderingSeekWeight;
    public float FollowSeekWeight;
    public float RadiusDetection;
    public float EscapeDistance;
    public float HitDistance;




    public GameObject Attractor;

	// Use this for initialization
	void Start ()
    {
        if (Attractor == null)
        {
            Debug.LogError("You don't have the attractor atached to the blackboard of the object");
        }
	
	}
	

}
