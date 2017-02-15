using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {


    public float Health = 100;
    public Texture2D cursor;
    private Vector2 OriginCursor;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = true;
        OriginCursor = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, OriginCursor, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }
}
