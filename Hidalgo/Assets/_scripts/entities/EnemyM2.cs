using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyM2 : MonoBehaviour, IDamageable, IPathfinder
{
    public int maxHealth = 100;
    protected int currentHealth;

    public float movementSpeed = 4f;

    protected Vector2 originalPosition;
    protected Rigidbody2D _rigidbody;
    protected Health health;
    protected Animator _animator;

    protected CharacterPathfindingMovementHandler _pathfinder;

    public abstract void Init();

    public virtual float TakeDamage(float value)
    {
        this.health.Damage((int)value);
        return this.health.GetHealth();
    }

    public void Die()
    {
        this.health.Die();
    }

    public bool IsDead()
    {
        return this.health.IsDead();
    }

    public virtual void SetTarget(Vector3 positionToReach)
    {
        _pathfinder.SetMovementPath(transform.position, positionToReach);
    
    }

    public virtual void ClearPath()
    {
        _pathfinder.SetTargetPosition(_pathfinder.GetPosition());
    }
}
