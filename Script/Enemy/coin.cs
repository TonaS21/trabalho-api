using Godot;
using System;

public partial class coin : Area2D
{
	public delegate void CoinCollected();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(CharacterBody2D body)
	{
 		if(body.Name == "Player")
		{
			EmitSignal("CoinCollected");
			QueueFree();
		}
	}
}
