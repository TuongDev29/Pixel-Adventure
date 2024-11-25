using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MainMonoBehaviour
{
    [SerializeField] private Animator _anim;
    public Animator anim => _anim;
    [SerializeField] private Collider2D _collider2D;
    public Collider2D Collider2D => _collider2D;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAnimator();
        this.LoadCollider2D();
    }

    private void LoadAnimator()
    {
        if (this._anim != null) return;
        this._anim = transform.GetComponentInChildren<Animator>();
        Debug.LogWarning("LoadAnimator", gameObject);
    }

    private void LoadCollider2D()
    {
        if (this._collider2D != null) return;
        this._collider2D = transform.GetComponent<Collider2D>();
        Debug.LogWarning("LoadCollider2D", gameObject);
    }

}
