
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private const float DetectInterval = 0.5f;
    private const float detectRadius = 5f;
    private const float PatrolPointChangeDestinationRange = 0.5f;

    private Vector3[] patrolPoints;
    private int index;
    private NavMeshAgent _agent;
    private float _detectIntervalTimer;
    private AIController _controller;

    public IdleState(Vector3[] patrolPoints, NavMeshAgent agent, AIController controller)
    {
        this.patrolPoints = patrolPoints;
        this._agent = agent;
        this._controller = controller;
        index = 0;
        agent.SetDestination(patrolPoints[index]);
    }

    public void Enter()
    {

    }

    public void Execute(float dt)
    {
        if (_agent.destination != null)
        {
            var directionVector = _agent.transform.position - _agent.destination;
            if (directionVector.sqrMagnitude < PatrolPointChangeDestinationRange)
            {
                index++;
                if (index >= patrolPoints.Length)
                {
                    index = 0;
                }
                _agent.SetDestination(patrolPoints[index]);
            }
        }
        var isPlayerDetected = DetectPlayer(dt);
        if (isPlayerDetected) _controller.ChangeToCombatState();
    }

    private bool DetectPlayer(float dt)
    {
        _detectIntervalTimer += dt;
        if (_detectIntervalTimer > DetectInterval)
        {
            _detectIntervalTimer = 0;
            RaycastHit playerHit;
            var colliders = Physics.OverlapSphere(_agent.transform.position, detectRadius, 1 << 7);
            if (colliders.Length > 0)
            {
                var direction = colliders[0].transform.position - _agent.transform.position;
                var isWallHit = Physics.Raycast(_agent.transform.position, direction, 100f, 1 << 6);

                return !isWallHit && colliders.Length > 0;
            }
        }
        return false;
    }

    public void Exit()
    {
        _agent.ResetPath();
    }
}

