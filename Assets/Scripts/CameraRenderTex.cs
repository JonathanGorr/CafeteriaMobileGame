using UnityEngine;
using System.Collections;

public class CameraRenderTex : MonoBehaviour {

	private Texture2D renderedTexture;
	public Material mat;

	// Use this for initialization
	void Awake () 
	{
		//create the texture with a given size
		renderedTexture = new Texture2D(Screen.width,Screen.height);

		mat.mainTexture = renderedTexture;
	}

	private void OnPostRender()
	{
		//read pixels of the texture, post rendering will retrieve the camera render
		renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0,0);

		//then apply to image
		renderedTexture.Apply();
	}
}
