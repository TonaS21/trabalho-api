using Godot;
using System;

public partial class diamond : Area2D
{
	AnimationPlayer animationPlayer;
	Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Start();
	}

	public override void _Process(double delta)
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("Diamond");
	}

	private void _on_body_entered(CharacterBody2D body)
	{
 		if(body.IsInGroup("player"))
		{
			QueueFree();
		}
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
}
