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

        ChangeToIdleState();
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }

    public void ChangeToCombatState()
    {
        _stateMachine.ChangeState(new CombatState(_agent, PlayerMovementController.Instance, projectileSpawn));
    }

    public void ChangeToFollowState(Vector3 lastSeenPlayerPos)
    {
        _stateMachine.ChangeState(new FollowState(_agent, PlayerMovementController.Instance, lastSeenPlayerPos));
    }

    public void ChangeToIdleState()
    {
        _stateMachine.ChangeState(new IdleState(patrolPoints.Select(s => s.position).ToArray(), _agent, this));
    }
}



