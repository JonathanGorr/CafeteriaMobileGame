using UnityEngine;
using System.Collections;

public class ShrinkSprite : MonoBehaviour {

	public Sprite full;
	public Sprite small;
	public Sprite renderedSprite;
	//private SpriteRenderer renderer;
	private Vector3 sizeSmall = new Vector3(0.2f,0.2f,0.2f);
	private Vector3 defaultSize;

	private void Awake()
	{
		//renderer = GetComponent<SpriteRenderer>();
		defaultSize = transform.localScale;
	}

	//shrink on enter
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(gameObject.tag == "Tray")
		{
			if(col.tag == "Window")
			{
				Shrink();
			}
		}
		if(col.tag == "Table")
		{
			Expand();
		}
	}

	//stick or drop on stay
	private void OnTriggerStay2D(Collider2D col)
	{
		if(col.tag == "Table")
		{
			rigidbody2D.isKinematic = true;
		}
		else
			rigidbody2D.isKinematic = false;
	}

	private void Shrink()
	{
		//renderer.sprite = small;
		transform.localScale = sizeSmall;
	}

	private void Expand()
	{
		//renderer.sprite = full;
		transform.localScale = defaultSize;
	}
}
