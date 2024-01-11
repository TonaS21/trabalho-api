using Godot;
using System;

public partial class tiro_attack : Area2D
{
	//public int level = 1; Para outra altura.
	public int hp = 1;
	public int vel = 1;
	public int tirodamage = 100;
	//public int knock_amount = 100;
	public int attack_size = 1;
	
	public Vector2 mousePos;
	public Vector2 angle = Vector2.Zero;
	public Vector2 target = Vector2.Zero;
	public Vector2 pos;
	public Vector2 direction;

	public PackedScene SwordSlash = GD.Load<PackedScene>("res://tiro_attack.tscn"); 
	
	public override void _Ready()
	{
		mousePos = GetGlobalMousePosition();
		var tiro = SwordSlash.Instantiate<Area2D>();
		pos = GetNode<CharacterBody2D>("Player").Position;
		Position = pos + pos.DirectionTo(mousePos) * 40;
		direction = Position.DirectionTo(mousePos);
		Position += direction*vel;
		AddSibling(tiro);
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
}

