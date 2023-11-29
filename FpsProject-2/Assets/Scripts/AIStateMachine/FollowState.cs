
using UnityEngine;
using UnityEngine.AI;


public class FollowState : IState
{
    private Vector3 _lastSeenPlayerPos;
    private NavMeshAgent _agent;
    private PlayerMovementController _player;
    private AIController _controller;

    public FollowState(NavMeshAgent agent, PlayerMovementController player, Vector3 lastSeenPlayerPos)
    {
        _lastSeenPlayerPos = lastSeenPlayerPos;
        _agent = agent;
        _player = player;
        _controller = agent.GetComponent<AIController>();
    }

    public void Enter()
    {
        _agent.SetDestination(_lastSeenPlayerPos);
    }

    public void Execute(float dt)
    {
        var vp = (_player.Position - _agent.transform.position);
        if (vp.sqrMagnitude < Mathf.Pow(GlobalStat.DetectRadius, 2))
        {
            bool canSeePlayer = !Physics.Raycast(_agent.transform.position, vp, 100f, 1 << 6);
            if (canSeePlayer) ChangeToCombatState();
        }
        var vls = (_lastSeenPlayerPos - _agent.transform.position);
        Debug.Log($"vls " + vls.sqrMagnitude);
        if (vls.sqrMagnitude < 1.5f)
        {
            ChangeToIdleState();
        }
    }

    private void ChangeToCombatState()
    {
        _controller.ChangeToCombatState();
    }

    private void ChangeToIdleState()
    {
        _controller.ChangeToIdleState();
    }

    public void Exit()
    {
    }
}

