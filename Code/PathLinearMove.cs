using Godot;
using System;

public class PathLinearMove : PathFollow2D
{
    [Export]
    public int Speed = 250;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      this.Offset += Speed * delta;
  }
}
