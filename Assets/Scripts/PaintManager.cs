using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaintManager : MonoBehaviour
{
    public static Color SelectedColor { get; private set; }

    public Slider slider;

    void Awake()
    {
        SelectedColor = Color.black;
    }
    void Start()
    {
    
    }

    void FixedUpdate()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //Debug.Log("Pressed");


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("HIT");
                Debug.Log(hit.collider.gameObject.name);

                var pallet = hit.collider.GetComponent<PaintCanvas>();
                if (pallet != null)
                {
                    //Debug.Log(hit.textureCoord);
                    //Debug.Log(hit.point);

                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    BrushAreaWithColor(pixelUV, SelectedColor, (int)slider.value);
                }
                else
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                    {
                       // Debug.Log("something was null");
                        return;
                    }

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    SelectedColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                   // Debug.Log("SelectedColor = " + SelectedColor);


                }
            }
            
        }
    }

    private void BrushAreaWithColor(Vector2 pixelUV, Color color, int size)
    {
        for (int x = -size; x < size; x++)
        {
            for (int y = -size; y < size; y++)
            {
                PaintCanvas.Texture.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, color);
            }
        }

        PaintCanvas.Texture.Apply();
    }
}
