using Godot;
using System;

public partial class world : Node2D
{
	public Timer timer;
	public Node2D SpawnLocation;
	
	public double timePassed = 0;
	public double multiplier = 5;
	public double multiplierCooldown = 0;
	
	public bool bossCanSpawn = true;

	public override void _Ready()
	{
	}
	
	public override void _Process(double delta) 
	{
		timePassed += delta;
		multiplierCooldown += delta;
		
		var ran = new Random();
		
		if (timePassed >= multiplier) {
			var enemy1 = GD.Load<PackedScene>("res://enemy.tscn");
			var enemy2 = GD.Load<PackedScene>("res://enemy_2.tscn");
			PackedScene[] mobs = {enemy1, enemy2};
			var enemies = mobs[ran.Next(0, 2)].Instantiate<CharacterBody2D>();
			AddChild(enemies);

			SpawnLocation = GetNode<Node2D>("SpawnLocation");
			enemies.Position = SpawnLocation.Position;
			var nodes = GetTree().GetNodesInGroup("spawn");
			var node = nodes[(int) ran.Next(0, nodes.Count - 1) ] as Marker2D;
			var position = node.Position;
			SpawnLocation.Position = position;
			
			timePassed = 0;
		}
		
		hud HUD = GetTree().Root.GetNode("World").GetNode("Player").GetNode("Camera2D").GetNode<hud>("HUD");
		if(HUD.seconds == 0 && HUD.min == 3 && bossCanSpawn == true) {  //quanto tempo passou
			var bos = GD.Load<PackedScene>("res://boss.tscn");
			var enem = bos.Instantiate<CharacterBody2D>();
			AddChild(enem);
			SpawnLocation = GetNode<Node2D>("SpawnLocation");
			enem.Position = SpawnLocation.Position;
			var nodes = GetTree().GetNodesInGroup("spawn");
			var node = nodes[(int) ran.Next(0, nodes.Count - 1) ] as Marker2D;
			var position = node.Position;
			SpawnLocation.Position = position;
			bossCanSpawn = false;
		}
		
		if(multiplierCooldown >= 10 && multiplier >= 0.75) {
			multiplier -= 0.25;
			multiplierCooldown = 0;
		}
	}
}

