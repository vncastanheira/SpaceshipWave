using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(ProjectileLauncher))]
[RequireComponent(typeof(RetroshipController))]
public class GridArrowsInput : GridInput
{
    PlayerAnimation anim;
    RetroshipController shipController;

    float horizontal;
    float vertical;

    float _timer;
    public float ProjectileDelay;
    
    public override void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        shipController = GetComponent<RetroshipController>();

        base.Start();
    }

    public override void GetInput()
    {
        Move();
        Shoot();

        Animate();
        base.GetInput();
    }

    /// <summary>
    /// Movement inputs
    /// </summary>
    void Move()
    {
        horizontal = vertical = 0;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            vertical -= 1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            vertical += 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            horizontal -= 1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            horizontal += 1;

        agent.Move(new Vector2(horizontal, vertical));
    }

    /// <summary>
    /// Animation setup
    /// </summary>
    void Animate()
    {
        anim.SetVelocity(agent.Direction.x * 30);

    }

    /// <summary>
    /// Shooting inputs
    /// </summary>
    void Shoot()
    {
        _timer -= Time.deltaTime;
        
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if (_timer < 0)
            {
                projectileLauncher.Launch(transform.forward);
                shipController.Shooting.Invoke();
                _timer = ProjectileDelay;
            }
        }
    }
}
