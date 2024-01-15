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
		var ran = new Random();

		var enemy1 = GD.Load<PackedScene>("res://enemy.tscn");
		var enemy2 = GD.Load<PackedScene>("res://enemy_2.tscn");
		PackedScene[] mobs = {enemy1, enemy2};
		var enemies = mobs[ran.Next(0, 2)].Instantiate<CharacterBody2D>();
		AddChild(enemies);

		SpawnLocation = GetNode<Node2D>("SpawnLocation");
		enemies.Position = SpawnLocation.Position;
		var nodes = GetTree().GetNodesInGroup("spawn");
		var node = nodes[(int) ran.Next(0, nodes.Count - 1) ] as Marker2D;
		var position = node.Position;
		SpawnLocation.Position = position;
	}
}

