using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [Header("PlayerController")]
    [SerializeField] private PlayerDataSO _playerDataSO;
    public PlayerDataSO PlayerDataSO => _playerDataSO;
    [SerializeField] private Rigidbody2D _rb;
    public Rigidbody2D rb => _rb;

    private StateMachine _playerState;
    public StateMachine PlayerState => _playerState;
    [SerializeField] private PlayerGroundedCheck _playerGroundedCheck;
    public PlayerGroundedCheck PlayerGroundedCheck => _playerGroundedCheck;
    [SerializeField] private PlayerWallSlidingCheck _playerWallSlidingCheck;
    public PlayerWallSlidingCheck PlayerWallSlidingCheck => _playerWallSlidingCheck;
    [SerializeField] private PlayerDirectional _playerDirectional;
    public PlayerDirectional playerDirectional => _playerDirectional;

    protected virtual void OnEnable()
    {
        transform.position = CheckPointManager.Instance.CurrentPoint.GetPosition();
    }

    protected virtual void Start()
    {
        _playerGroundedCheck = new PlayerGroundedCheck(this);
        _playerWallSlidingCheck = new PlayerWallSlidingCheck(this);
        _playerDirectional = new PlayerDirectional(this);

        _playerState = new PlayerStateMachine(this);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody2D();
    }

    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = GetComponent<Rigidbody2D>();
        Debug.LogWarning("LoadRigidbody2D", gameObject);
    }

    protected virtual void Update()
    {
        _playerState?.ExcuteState();

        if (Input.GetKeyDown(KeyCode.F)) PlayerState.ChangeState(EPlayerState.Hurt);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerState.stateAction is ITrigger2DState stateTriggerHandle)
        {
            stateTriggerHandle.OnTriggerEnter2D(other);
        }
    }
}
