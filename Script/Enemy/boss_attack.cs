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
	}

	public override void _Process(double delta)
	{	
		Position += direction*velocity;
	}
	
	private void _on_visible_on_screen_enabler_2d_screen_exited()
	{
		QueueFree();
	}
}
