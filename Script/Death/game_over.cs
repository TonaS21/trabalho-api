using Godot;
using System;

public partial class game_over : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_return_pressed()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}
}



