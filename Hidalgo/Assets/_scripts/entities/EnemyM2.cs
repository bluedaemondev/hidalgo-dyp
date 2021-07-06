using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyM2 : MonoBehaviour, IDamageable, IPathfinder
{
    public int maxHealth = 100;
    protected int currentHealth;

    public float movementSpeed = 4f;

    protected Vector2 originalPosition;
    [SerializeField]
    protected Vector2 targetPosition;

    protected Rigidbody2D _rigidbody;
    protected Health health;
    protected Animator _animator;

    //protected CharacterPathfindingMovementHandler _pathfinder;

    public abstract void Init();

    public virtual float TakeDamage(float value)
    {
        this.health.Damage((int)value);
        return this.health.GetHealth();
    }

    public void Die()
    {
        WaveSystem.instance.OnEnemyDied();
        this.health.Die();
    }

    public bool IsDead()
    {
        return this.health.IsDead();
    }

    public virtual void SetTarget(Vector3 positionToReach)
    {
        this.targetPosition = positionToReach;
        //Debug.Log("Setting path to target position = " + targetPosition);
    }

    /// <summary>
    /// fuera de uso
    /// </summary>
    public virtual void ClearPath()
    {
        this.targetPosition = Vector2.positiveInfinity;
    }
}
