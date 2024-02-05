using Godot;
using System;

public partial class victory : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}
}



