using Godot;
using System;

public class Player : Area2D
{
    [Signal]
    public delegate void Hit();
    
    [Export] public int Speed = 400;

    private Vector2 _screenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        Hide();
        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void Start(Vector2 startPosition)
    {
        Position = startPosition;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    private void OnBodyEntered(Node body)
    {
        Hide();
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        EmitSignal(nameof(Hit));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var velocity = new Vector2();
        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
        }

        // var animatedSprites = GetNode<AnimatedSprite>("AnimatedSprite");
        // if (velocity.Length() <= 0)
        // {
        //     animatedSprites.Stop();
        //     return;
        // }
        //
        // if (velocity.x != 0)
        // {
        //     animatedSprites.FlipV = false;
        //
        //     animatedSprites.Animation = "walk";
        //     animatedSprites.FlipH = velocity.x < 0;
        // } else if (velocity.y != 0)
        // {
        //     animatedSprites.Animation = "up";
        //     animatedSprites.FlipV = velocity.y > 0;
        // }
        //
        // animatedSprites.Play();

        velocity = velocity.Normalized();
        Position += velocity * delta * Speed;
        Position = new Vector2(
            Mathf.Clamp(Position.x, 0, _screenSize.x),
            Mathf.Clamp(Position.y, 0, _screenSize.y));
    }
}