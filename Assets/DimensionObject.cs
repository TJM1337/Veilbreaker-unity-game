using UnityEngine;

// Attach this to any object that should only exist in one dimension.
// Set "visibleIn" in the Inspector to either A or B.
public class DimensionObject : MonoBehaviour
{
    [Tooltip("Which dimension is this object visible/active in?")]
    public DimensionManager.Dimension visibleIn = DimensionManager.Dimension.A;

    void Awake()
    {
        DimensionManager.OnDimensionChanged += OnDimensionChanged;
    }

    void OnDestroy()
    {
        DimensionManager.OnDimensionChanged -= OnDimensionChanged;
    }

    void Start()
    {
        if (DimensionManager.Instance != null)
            UpdateVisibility(DimensionManager.Instance.CurrentDimension);
    }

    void OnDimensionChanged(DimensionManager.Dimension newDimension)
    {
        UpdateVisibility(newDimension);
    }

    void UpdateVisibility(DimensionManager.Dimension dim)
    {
        bool shouldBeVisible = (dim == visibleIn);

        // Toggle renderer so it shows/hides visually
        Renderer rend = GetComponent<Renderer>();
        if (rend != null) rend.enabled = shouldBeVisible;

        // Toggle collider so you can't walk through hidden objects
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = shouldBeVisible;
    }
}