using Godot;
using System;

public partial class tiro_attack : Area2D
{
	//public int level = 1; Para outra altura.
	public int hp = 1;
	public int speed;
	public int damage;
	//public int knock_amount = 100;
	public int attack_size = 1;
	
	public Vector2 mousePos;
	public Vector2 angle = Vector2.Zero;
	public Vector2 target = Vector2.Zero;

	public PackedScene SwordSlash = GD.Load<PackedScene>("res://tiro_attack.tscn");

	public CharacterBody2D Player; 
	
	public override void _Ready()
	{
		mousePos = GetGlobalMousePosition();
		speed = 150;
		damage = 100;
		angle = GlobalPosition.DirectionTo(target);
		Rotation = angle.Angle() + Mathf.DegToRad(135);
	}

	public override void _Process(double delta)
	{
		float deltaFloat = (float)delta;
		Position += angle*speed*deltaFloat;
	}

	public void enemy_hit(int charge = 1) {
		hp -= charge;
		if (hp <= 0) {
			QueueFree();
		}
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
}

