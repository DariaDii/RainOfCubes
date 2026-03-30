using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public void ChangeToRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }
}