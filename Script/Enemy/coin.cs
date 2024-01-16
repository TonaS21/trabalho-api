using Godot;
using System;

public partial class coin : Area2D
{
	AnimationPlayer animationPlayer;
	public int coinsCollected = 0;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("Coin");
	}
	
	private void _on_body_entered(CharacterBody2D body)
	{
 		if(body.Name == "Player")
		{
			coinsCollected = coinsCollected + 1;
			GD.Print(coinsCollected);
			QueueFree();
		}
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
}
