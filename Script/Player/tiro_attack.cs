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
	public bool pl_attack_cooldown = true;
	
	public Vector2 mousePos;
	public Vector2 pos;
	public Vector2 direction;
	public Timer timer;

	public PackedScene SwordSlash = GD.Load<PackedScene>("res://tiro_attack.tscn"); 
	
	public override void _Ready()
	{
		if(Input.IsActionJustPressed("attack")) {
			mousePos = GetGlobalMousePosition();
			var tiro = SwordSlash.Instantiate<Area2D>();
			pos = GetNode<CharacterBody2D>($"..").Position;
			Position = pos + pos.DirectionTo(mousePos) * 40;
			direction = Position.DirectionTo(mousePos);
			Position += direction*vel;
			AddSibling(tiro);
			this.Visible = true;
			pl_attack_cooldown = false;
		}
	}

	public override void _Process(double delta)
	{	
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
	
	private void _on_cooldown_timeout()
	{
		pl_attack_cooldown = true;
	}	
}
