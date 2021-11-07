using System;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    #region Enums

    // As the sprites used have different features if the character is male or female, this is important
    public enum BodyType
    {
        Male,
        Female
    }

    #endregion
    
    #region Setup

    // Components
    protected Rigidbody2D Rb2d;

    // Variables
    public virtual float Speed => 5f;

    // Methods
    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();
    }

    #endregion
    
    #region Animation Handling

    [HideInInspector] private Animator _animator;

    // Animator Parameter names
    public readonly int AnimSpeed = Animator.StringToHash("Speed");
    public readonly int AnimChangedDirection = Animator.StringToHash("ChangedDirection");
    public readonly int AnimDirection = Animator.StringToHash("Direction");

    ///// Event handlers
    // Delegates
    public delegate void ChangedDirectionHandler(object source, EventArgs args);

    // Events
    public event ChangedDirectionHandler ChangedDirection;

    // Calling Event Methods
    /// <summary>
    /// Calls for all depending clothing animation to change direction
    /// </summary>
    /// <param name="source"></param>
    protected virtual void OnChangedDirection(object source)
    {
        ChangedDirection?.Invoke(source, EventArgs.Empty);
    }

    /// <summary>
    /// Force the event "Change direction" to trigger, basically, it resync the animation 
    /// </summary>
    public void ForceChangeDirection()
    {
        OnChangedDirection(this);
    }

    #endregion
    
    #region Movement Handling

    [HideInInspector] public int lastDirection = 2; // 0 = up; 1 = right; 2 = down; 3 = left
    [HideInInspector] public bool moving = false;

    /// <summary>
    /// Move towards the informed direction
    /// </summary>
    /// <param name="dir">Direction of movement</param>
    protected void Move(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            var d = GetDirection(dir);
            if (d != lastDirection || !moving)
            {
                _animator.SetFloat(AnimSpeed,
                    Speed); // Currently, the Speed only affects the state, not the animation speed TODO: Animation speed with movement speed
                _animator.SetInteger(AnimDirection, d);
                _animator.SetTrigger(AnimChangedDirection);

                moving = true;
                lastDirection = d;

                OnChangedDirection(this); // Calls for all the clothes attached to this body to change direction
            }

            //var targetPosition = Rb2d.position + (Vector2) dir * Time.fixedDeltaTime * Speed;
            Rb2d.velocity = (Vector2) dir * Time.fixedDeltaTime * Speed;
        }
        else
        {
            if (moving)
            {
                moving = false;
                _animator.SetFloat(AnimSpeed, 0);
                OnChangedDirection(this); // Call all character's clothes animation to update to a stopped state
            }
        }
    }

    /// <summary>
    /// Get an integer direction, this can be used on the animator
    /// </summary>
    /// <param name="vDirection">Vector direction</param>
    /// <returns></returns>
    private int GetDirection(Vector3 vDirection)
    {
        if (vDirection.y != 0)
        {
            return vDirection.y > 0 ? 0 : 2;
        }
        else if (vDirection.x != 0)
        {
            return vDirection.x > 0 ? 1 : 3;
        }


        Debug.Log("Oops");
        return lastDirection;
    }

    #endregion
}