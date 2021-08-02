using Godot;
using System;
using System.Threading.Tasks;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("StartButton").Connect("pressed", this, nameof(On_StartButton_Pressed));
        Reset();
    }

    public void Reset()
    {
        GetNode<Button>("StartButton").Show();
        GetNode<Label>("MessageLabel").Hide();
    }

    public SignalAwaiter ShowMessage(string message, float duration)
    {
        var msgLabel = GetNode<Label>("MessageLabel");
        msgLabel.Text = message;
        msgLabel.Show();
        return ToSignal(GetTree().CreateTimer(duration), "timeout");
        //msgLabel.Hide();
    }

    public void On_StartButton_Pressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }
}
