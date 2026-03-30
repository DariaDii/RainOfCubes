using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    
    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = _renderer.materials[0].color;
    }

    public void ChangeToRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void ResetState()
    {
        _renderer.material.color = _defaultColor;
    }
}