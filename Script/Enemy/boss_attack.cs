using Godot;
using System;

public partial class boss_attack : Area2D
{
	public int velocity;
	public Timer timer;
	public Vector2 direction;

	public PackedScene BossAttack = GD.Load<PackedScene>("res://boss_attack.tscn"); 
	
	public override void _Ready()
	{
		this.velocity = 10;
		timer = GetNode<Timer>("AttackTimer");
		timer.Start();
	}

	public override void _Process(double delta)
	{	
		Position += direction*velocity;
	}
	
	private void _on_attack_timer_timeout()
	{
		QueueFree();
	}
}

