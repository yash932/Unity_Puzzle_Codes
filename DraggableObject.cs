using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    private Vector3 _offset;
    private Camera _cam;
    private bool _isDragging;
    private Collider2D _collider;

    private void Awake()
    {
        _cam = Camera.main;
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseDown()
    {
        if (IsPointerOverUI()) return;

        _isDragging = true;

        if (_collider != null)
            _collider.enabled = false;  // Disable collider while dragging to allow tiles underneath to be detected

        _offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        if (!_isDragging) return;

        _isDragging = false;

        if (_collider != null)
            _collider.enabled = true;  // Re-enable collider after dropping

        SnapToGrid();
    }

    private void DragObject()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        transform.position = mouseWorldPos + _offset;
    }

    private Vector3 GetMouseWorldPosition()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
            return _cam.ScreenToWorldPoint(Input.GetTouch(0).position) + Vector3.forward * 10;
#endif
        return _cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }

    private void SnapToGrid()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        pos.z = 0f;
        transform.position = pos;
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}

