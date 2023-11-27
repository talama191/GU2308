using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    [Header("Idle,Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float walkSpeed;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;

    [Header("Combat")]
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] float damage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float gravity;

    private float _attackTimer;
    private EnemyBulletSupply _bulletSupply;
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _bulletSupply = EnemyBulletSupply.Instance;
        _stateMachine = new StateMachine();

        _stateMachine.ChangeState(new IdleState(patrolPoints.Select(s => s.position).ToArray(), _agent, this));
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }

    public void ChangeToCombatState()
    {
        _stateMachine.ChangeState(new CombatState(_agent, PlayerMovementController.Instance));
    }

    private void CombatControl()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer > attackCooldown)
        {
            var playerPos = PlayerMovementController.Instance.Position;
            playerPos.y = projectileSpawn.position.y;
            var direction = playerPos - projectileSpawn.position;
            HandleShooting(direction);
        }
    }

    private void HandleShooting(Vector3 direction)
    {
        _attackTimer = 0;
        var bullet = _bulletSupply.GetSupply();
        bullet.transform.position = projectileSpawn.position;
        bullet.ShootProjectile(direction, projectileSpeed, damage, gravity);
    }
}



