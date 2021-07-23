using Godot;
using System;

public class MobSwimmer : Mob
{
    public override void _Ready()
    {
        base._Ready();
        
        SetAnimation("swim");
        Speed = 100;
    }
}
