using Godot;
using System;

public class Main : Node
{
	[Export] public PackedScene[] Mobs;
	[Export] public float DirectionalJitter = 0f;

	private int _score;
	private Random _random = new Random();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
#if DEBUG
		Console.WriteLine($"{nameof(Main)}.{nameof(_Ready)}");
#endif
		
		// connect to all signals
		GetNode<HUD>("HUD").Connect(nameof(HUD.StartGame), this, nameof(GameStart));
		GetNode<Player>(nameof(Player)).Connect(nameof(Player.Hit), this, nameof(GameOver));
		//GetNode<Timer>("StartTimer").Connect("timeout", this, nameof(GameStart));	
		GetNode<Timer>("MobTimer").Connect("timeout", this, nameof(SpawnMob));
		GetNode<Timer>("ScoreTimer").Connect("timeout", this, nameof(Score));
	}

	public void GameStart()
	{
#if DEBUG
		Console.WriteLine($"{nameof(Main)}.{nameof(GameStart)}");
#endif


		_score = 0;
		var player = GetNode<Player>(nameof(Player));
		player.Start(GetNode<Position2D>("StartPosition").Position);

		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	public void SpawnMob()
	{
		var pathFollow = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		var position = pathFollow.Position;
		
#if DEBUG
		Console.WriteLine($"{nameof(Main)}.{nameof(SpawnMob)}@{position}");
#endif


		var newMob = Mobs[_random.Next(0, Mobs.Length)].Instance<Enemy>();
		AddChild(newMob);
		
		float direction = pathFollow.Rotation + Mathf.Pi / 2;
		newMob.Initialize(position, direction, DirectionalJitter);
	}

	public void Score()
	{
		_score++;
	}

	public async void GameOver()
	{
#if DEBUG
		Console.WriteLine($"{nameof(Main)}.{nameof(GameOver)}");
#endif
		
		var player = GetNode<Player>(nameof(Player));
		player.Hide();
		
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		var hud = GetNode<HUD>("HUD");
		await hud.ShowMessage("Game Over", 2f);
		hud.Reset();
	}
}
