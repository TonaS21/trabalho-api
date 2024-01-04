using Godot;
using System;

public partial class enemy_spawner : Node2D
{
	public Timer timer;
	public PackedScene enemy2 = GD.Load<PackedScene>("res://enemy.tscn");

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Start();
	}

	private void _on_timer_timeout()
	{
		var enemy1 = enemy2.Instantiate();
		AddChild(enemy1);
	}
}
