using UnityEngine;

public abstract class MainMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.ControllerLoader();
    }

    protected virtual void Awake()
    {
        this.ControllerLoader();
    }

    protected void ControllerLoader()
    {
        this.Begin();
        this.LoadComponent();
        this.ResetValue();
        this.EndLoad();
    }

    protected virtual void Begin() { }

    protected virtual void LoadComponent() { }

    protected virtual void ResetValue() { }

    protected virtual void EndLoad() { }

    protected void AutoLoad<T>(ref T _object, T _objectValue)
    {
        if (_object != null) return;
        _object = _objectValue;
        Debug.LogWarning("AutoLoad => " + typeof(T), gameObject);
    }
}
