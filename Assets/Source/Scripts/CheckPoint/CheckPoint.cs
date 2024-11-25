using UnityEngine;

public abstract class CheckPoint : MainMonoBehaviour
{
    [SerializeField] protected Collider2D _collider2D;
    public bool IsSaved { get; private set; } = false;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTrigger2D();
    }

    private void LoadTrigger2D()
    {
        if (this._collider2D != null) return;

        this._collider2D = GetComponent<Collider2D>();
        this._collider2D.isTrigger = true;
        Debug.LogWarning("LoadTrigger2D", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (this.IsSaved) return;
        CheckPointManager.Instance.SavePoint(this);
        this.OnSaved();
        this.IsSaved = true;
    }

    public virtual Vector2 GetPosition()
    {
        return transform.position;
    }

    protected abstract void OnSaved();
}
