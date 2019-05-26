using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintCanvas : MonoBehaviour
{
    public static Texture2D Texture { get; private set; }


    public static byte[] GetAllTextureData()
    {
        return Texture.GetRawTextureData();
    }

    private void Start()
    {
        PrepareTemporaryTexture();
        float quadHeight = Camera.main.orthographicSize * 2.0f;
        float quadWidth = quadHeight * Screen.width / Screen.height;
        transform.localScale = new Vector3(quadWidth, quadHeight, 1);
    }


    private void PrepareTemporaryTexture()
    {
        Texture = (Texture2D)GameObject.Instantiate(GetComponent<Renderer>().material.mainTexture);
        Debug.Log(Texture.width);
        Debug.Log(Texture.height);
        Texture.Resize(1000, 1000);
        Debug.Log(Texture.width);
        Debug.Log(Texture.height);
        GetComponent<Renderer>().material.mainTexture = Texture;
    }
}