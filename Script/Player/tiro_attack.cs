using Godot;
using System;

public partial class tiro_attack : Area2D
{
	//public int level = 1; Para outra altura.
	public int velocity;
	//public int knock_amount = 100;
	public Timer timer;
	public Vector2 direction;
	public bool pl_attack_cooldown = true;

	public PackedScene SwordSlash = GD.Load<PackedScene>("res://tiro_attack.tscn"); 
	
	public override void _Ready()
	{
		this.velocity = 10;
	}

	public override void _Process(double delta)
	{	
		Position += direction*velocity;
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
	
	//private void _on_cooldown_timeout()
	//{
	//	pl_attack_cooldown = true;
	//}	
}
