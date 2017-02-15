using UnityEngine;
using System.Collections;
using Steerings;

public class FixeRot : MonoBehaviour {

    public KinematicState Parent;
    private SpriteRenderer MySprite;
    private float CompareRot;
    // Use this for initialization
	void Start () {
        MySprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        this.transform.rotation = Quaternion.identity;

        if (Parent.linearVelocity.x >= 0)
        {
            MySprite.flipX = false;
        }
        else
        {
            MySprite.flipX = true;
        }
	
	}
}
