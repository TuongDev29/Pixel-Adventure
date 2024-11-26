using UnityEngine;

public abstract class Directional
{
    protected Transform transform;
    [SerializeField] private bool _isFacingRight = true;
    public bool IsFacingRight => _isFacingRight;

    public Directional(MonoBehaviour monoBehaviour)
    {
        this.transform = monoBehaviour.transform;
    }

    public void ToggleDirection()
    {
        this.ChangeDirection(new Vector2(this._isFacingRight ? -1 : 1, 0));
    }

    public void ChangeDirection(Vector2 direction)
    {
        if (direction.x == 0) return;
        bool isRightDirection = direction.x > 0;
        if (this._isFacingRight == isRightDirection) return;

        this._isFacingRight = isRightDirection;
        float yRot = this._isFacingRight ? 0 : 180;

        transform.rotation = Quaternion.Euler(transform.rotation.x, yRot, transform.rotation.z);
    }
}
