using Godot;
using System;

public class MobFlier : Mob
{
    public override void _Ready()
    {
        base._Ready();
        
        SetAnimation("fly");
        Speed = 225;
    }
}
