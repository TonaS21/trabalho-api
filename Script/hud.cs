using Godot;
using System;

public partial class hud : Node2D
{
	public int seconds = 0;
	public int min = 0;
	public int Dseconds = 00;
	public int Dmin = 5;
	public int coinsCollected = 0;

	public int CoinsValue = 1;

	public override void _Process(double delta)
	{
		var coinsDisplay = GetNode<Label>("Moedas");
		coinsDisplay.Text = coinsCollected + " coins";
	}
	
	public override void _Ready()
	{
		TimerReset();
	}
	
	private void _on_game_timer_timeout()
	{
		if(seconds == 0) {
			if(min > 0) {
				min -= 1;
				seconds = 60;
			}
		}
		seconds -= 1;
		var Label = GetNode<Label>("Time");
		if (seconds < 10) {
			Label.Text = min.ToString() + ":" + "0" + seconds.ToString();
		} else {
			Label.Text = min.ToString() + ":" + seconds.ToString();
		}
		
		if(seconds == 0 && min == 0) {
			GetTree().ChangeSceneToFile("res://victory.tscn");
		}
	}
	
	public void TimerReset() {
		seconds = Dseconds;
		min = Dmin;
	}

	public void addCoin() {
		coinsCollected += CoinsValue;
		var coinsDisplay = GetNode<Label>("Moedas");
		coinsDisplay.Text = coinsCollected + " coins";
	}

	private void _on_upgrade_1_pressed()
	{
		player Player = GetTree().Root.GetNode("World").GetNode<player>("Player");
		
		if(coinsCollected >= 15) //&& Input.IsActionPressed("upgrade1")
		{
			coinsCollected -= 15;
			Player.swordDamage += 5;
		}
	}


	private void _on_upgrade_2_pressed()
	{
		player Player = GetTree().Root.GetNode("World").GetNode<player>("Player");
		
		if(coinsCollected >= 15) //&& Input.IsActionPressed("upgrade2")
		{
			coinsCollected -= 15;
			Player.tiroDamage += 5;
		}
	}


	private void _on_upgrade_3_pressed()
	{
		player Player = GetTree().Root.GetNode("World").GetNode<player>("Player");
		if(coinsCollected >= 10 && Player.health <= 90) { //&& Input.IsActionPressed("upgrade3")
			Player.health += 10;
			coinsCollected -= 10;
		}
	}
	
	public override void _Input(InputEvent @event)
 	{
		if (@event.IsActionPressed("pause"))
		{
			GetTree().ChangeSceneToFile("res://main.tscn");
		}         
	}
}
