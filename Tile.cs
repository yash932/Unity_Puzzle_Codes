using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _offsetColor = Color.gray;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private void Awake()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<SpriteRenderer>();

            if (_renderer == null)
            {
                Debug.LogError($"Tile '{name}' is missing a SpriteRenderer component!");
            }
        }

        if (_highlight != null)
        {
            _highlight.SetActive(false);
        }
    }

    public void Init(bool isOffset)
    {
        if (_renderer != null)
        {
            _renderer.color = isOffset ? _offsetColor : _baseColor;
        }
    }

    private void OnMouseEnter()
    {
        if (_highlight != null)
        {
            _highlight.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (_highlight != null)
        {
            _highlight.SetActive(false);
        }
    }
}
