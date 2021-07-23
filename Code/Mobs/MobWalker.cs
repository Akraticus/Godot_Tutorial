using Godot;
using System;

public class MobWalker : Mob
{
    public override void _Ready()
    {
        base._Ready();
        
        SetAnimation("walk");
        Speed = 150;
    }
}
