using Godot;
using System;

public partial class world : Node2D
{
	public Timer timer;
	public Node2D SpawnLocation;

	public override void _Ready()
	{
		timer = GetNode<Timer>("SpawnTimer");
		timer.Start();
	}

	private void _on_spawn_timer_timeout()
	{
		var scene = GD.Load<PackedScene>("res://enemy.tscn");
		var enemy1 = scene.Instantiate<CharacterBody2D>();
		AddChild(enemy1);

		var ran = new RandomNumberGenerator();

		SpawnLocation = GetNode<Node2D>("SpawnLocation");
		enemy1.Position = SpawnLocation.Position;
		var nodes = GetTree().GetNodesInGroup("spawn");
		var node = nodes[(int) ran.Randi() % nodes.Count] as Marker2D;
		var position = node.Position;
		SpawnLocation.Position = position;
	}
}

