using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeToRandomColor(Renderer renderer)
    {
        renderer.material.color = Random.ColorHSV();
    }
}