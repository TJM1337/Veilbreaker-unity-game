using UnityEngine;
using System;

public class DimensionManager : MonoBehaviour
{
    public static DimensionManager Instance { get; private set; }

    public enum Dimension { A, B }

    public Dimension CurrentDimension { get; private set; } = Dimension.A;

    // Any script can subscribe to this to react to a dimension switch
    public static event Action<Dimension> OnDimensionChanged;

    [Header("Settings")]
    public KeyCode switchKey = KeyCode.Q;
    public float switchCooldown = 0.3f; // prevent spam-switching

    private float lastSwitchTime = -999f;

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey) && Time.time - lastSwitchTime >= switchCooldown)
        {
            SwitchDimension();
        }
    }

    public void SwitchDimension()
    {
        CurrentDimension = (CurrentDimension == Dimension.A) ? Dimension.B : Dimension.A;
        lastSwitchTime = Time.time;

        // Notify all listeners
        OnDimensionChanged?.Invoke(CurrentDimension);

        Debug.Log($"Switched to Dimension {CurrentDimension}");
    }
}