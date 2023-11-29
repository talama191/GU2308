
using UnityEngine;
using UnityEngine.AI;

public class CombatState : IState
{
    const float Damage = 5;
    const float ProjectileSpeed = 3;
    const float Gravity = -1;
    const float AttackCooldown = 5f;
    const float AttackRange = 10f;

    private NavMeshAgent _agent;
    private PlayerMovementController _player;
    private Transform _projectileSpawn;
    private EnemyBulletSupply _bulletSupply;
    private AIController _controller;
    private float _attackTimer;

    public CombatState(NavMeshAgent agent, PlayerMovementController player, Transform projectileSpawn)
    {
        _agent = agent;
        _controller = agent.GetComponent<AIController>();
        _player = player;
        _projectileSpawn = projectileSpawn;
        _bulletSupply = EnemyBulletSupply.Instance;
        _attackTimer = AttackCooldown;
    }

    public void Enter()
    {
        _agent.SetDestination(_player.Position);
    }

    public void Execute(float dt)
    {
        _attackTimer += dt;
        var vp = (_player.Position - _agent.transform.position);
        var sqrDist = vp.sqrMagnitude;
        if (sqrDist <= AttackRange * AttackRange)
        {
            _agent.ResetPath();
            CombatControl(dt);
        }
        else
        {
            _agent.SetDestination(_player.Position);
        }
        bool canSeePlayer = !Physics.Raycast(_agent.transform.position, vp, 100f, 1 << 6);
        if (!canSeePlayer)
        {
            SwitchToFollowState();
        }
    }

    private void SwitchToFollowState()
    {
        _controller.ChangeToFollowState(_player.Position);
    }

    public void Exit()
    {

    }

    private void CombatControl(float dt)
    {


        if (_attackTimer > AttackCooldown)
        {
            var playerPos = PlayerMovementController.Instance.Position;
            playerPos.y = _projectileSpawn.position.y;
            var direction = playerPos - _projectileSpawn.position;
            HandleShooting(direction);
        }
    }

    private void HandleShooting(Vector3 direction)
    {
        _attackTimer = 0;
        var bullet = _bulletSupply.GetSupply();
        bullet.transform.position = _projectileSpawn.position;
        bullet.ShootProjectile(direction, ProjectileSpeed, Damage, Gravity);
    }
}

