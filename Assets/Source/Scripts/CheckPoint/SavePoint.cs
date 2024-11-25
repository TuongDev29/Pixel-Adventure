using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : CheckPoint
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _spawnPoint;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAnimator();
        this.LoadSpawnPoint();
    }

    private void LoadAnimator()
    {
        if (this._anim != null) return;

        this._anim = transform.GetComponentInChildren<Animator>();
        Debug.LogWarning("LoadAnimator", gameObject);
    }

    private void LoadSpawnPoint()
    {
        if (this._spawnPoint != null) return;
        this._spawnPoint = transform.Find("SpawnPoint");
        Debug.LogWarning("LoadSpawnPoint", gameObject);
    }

    protected override void OnSaved()
    {
        this._anim.SetTrigger("flagOut");
    }

    public override Vector2 GetPosition()
    {
        return _spawnPoint.position;
    }
}
