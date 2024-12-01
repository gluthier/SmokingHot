using System.Collections;
using UnityEngine;

public class ColorInterpolator : MonoBehaviour
{
    Color lerpedColor = Color.blue;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        lerpedColor = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
        renderer.material.color = lerpedColor;
    }    
}
