using UnityEngine;
using System.Collections;

public class MoveObjects : MonoBehaviour {

	private Vector3 mousePos;
	private Vector3 dragOffset;
	private Transform myTransform;
	private bool onTable, dropped, onTray;

	private Animator anim;
	private Rigidbody2D rigidBody;
	private LevelManager manager;
	private GameObject foodGroup;
	private BoxCollider2D collider;

	private float
	top = 1.6f,
	bottom = -1.6f,
	left = -12.85f,
	right = -7.148f,
	x,y;

	//child to tray parent
	//transform.parent = otherGameObject.transform
	//detach
	//transform.parent = null;
	
	private void Awake()
	{
		//cache
		myTransform = transform;
		//import
		anim = GetComponent<Animator>();
		manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		foodGroup = GameObject.Find("Food");
		rigidBody = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		//update these for better performance
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
	}

	private void OnMouseDown()
	{
		if(manager.inMenu == false)
		{
			//when picked up, no longer parented
			myTransform.parent = foodGroup.transform;

			//sprite position becomes mouse position
			mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (x, y, 10f));
			//flatten mouse z position(for selection accuracy)
			mousePos.z = 0f;

			//drag offset is the distance between the mouse pointer and the sprite held
			dragOffset = mousePos - myTransform.position;

			//play animation for everything but tray
			if(gameObject.tag != "Tray")
			{
				//play anim
				anim.SetTrigger("Expand");
			}

			//sprite is picked up when clicked on
			dropped = false;
		}
	}

	private void OnMouseDrag()
	{
		//if in menu, freeze interaction
		if(manager.inMenu == false)
		{
			//sprite.transform = pointer location
			mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (x, y, 10f));
			mousePos.z = 0f;

			Vector3 adjustedPos = mousePos - dragOffset;

			//bounds
			if(adjustedPos.x < left)
				adjustedPos.x = left;
			else if(adjustedPos.x > right)
				adjustedPos.x = right;
			else if(adjustedPos.y > top)
				adjustedPos.y = top;
			else if(adjustedPos.y < bottom)
				adjustedPos.y = bottom;

			//if dragging, not dropped
			dropped = false;

			//position is adjusted position
			myTransform.position = adjustedPos;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if(manager.inMenu == false)
		{
			//if on table, stick/have gravity
			if(col.tag == "Table")
			{
				if(rigidBody != null)
					rigidbody2D.isKinematic = true;
			}

			if(gameObject.tag == "Tray")

			//on tray
			if(gameObject.tag == "Food" && col.tag == "Tray")
			{
				onTray = true;
			}

			//food sticks to tray if not held
			if(dropped)
			{
				if(col.tag == "Food" && gameObject.tag == "Tray")
				{
					col.transform.parent = myTransform;
					rigidbody2D.isKinematic = true;
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if(manager.inMenu == false)
		{
			//depart food from tray if out of tray
			myTransform.parent = foodGroup.transform;

			//off tray
			if(gameObject.tag == "Tray" && col.tag == "Food")
			{
				onTray = false;
			}
		}
	}

	private void OnMouseUp()
	{
		if(manager.inMenu == false)
		{
			if(rigidBody != null)
				rigidbody2D.isKinematic = false;

			//play animation for everything but tray
			if(gameObject.tag != "Tray")
			{
				//play drop anim
				anim.SetTrigger("Shrink");
			}

			dropped = true;
		}
	}
}
