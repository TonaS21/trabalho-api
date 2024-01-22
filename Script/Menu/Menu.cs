using Godot;
using System;

public partial class Menu : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_new_game_pressed()
	{
		GetTree().ChangeSceneToFile("res://world.tscn");
	}


	private void _on_quit_pressed()
	{ 
		GetTree().Quit();
	}
}



