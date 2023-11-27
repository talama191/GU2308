
using UnityEngine.AI;

public class CombatState : IState
{
    private NavMeshAgent _agent;
    private PlayerMovementController _player;

    public CombatState(NavMeshAgent agent, PlayerMovementController player)
    {
        _agent = agent;
        _player = player;
    }

    public void Enter()
    {
        _agent.SetDestination(_player.Position);
    }

    public void Execute(float dt)
    {
        _agent.SetDestination(_player.Position);
    }

    public void Exit()
    {
    }
}

