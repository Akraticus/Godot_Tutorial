using Godot;
using System;

public class Mob : RigidBody2D
{
    private static Random _random = new Random();

    [Export] public int Speed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<VisibilityNotifier2D>(nameof(VisibilityNotifier2D))
            .Connect("screen_exited", this, nameof(OnScreenExited));
    }

    protected void SetAnimation(string animationName)
    {
        var animatedSprite = GetNode<AnimatedSprite>(nameof(AnimatedSprite));
        animatedSprite.Animation = animationName;
    }

    public void OnScreenExited()
    {
#if DEBUG
        Console.WriteLine($"{nameof(Mob)}.{nameof(OnScreenExited)}");
#endif
        
        QueueFree();
    }

    public void Initialize(Vector2 position, float direction, float jitter = 0f)
    {
        Position = position;
        Rotation = direction + _random.RandRange(-jitter, jitter);
        
        LinearVelocity = new Vector2(Speed, 0).Rotated(Rotation);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    //     
    // }
}