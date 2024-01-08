using Godot;
using GodotPlugins.Game;
using System;

public partial class player : CharacterBody2D
{
	public const float speed = 300f; //original 200
	
	public AnimationPlayer animationPlayer;
	public Timer timer;
	public ProgressBar Health;
	
	public bool en_range = false;
	public bool en_attack_cooldown = true;
	public bool playerAlive = true;

	public int health = 100;
	public int damage = 20;

	public void Movement() {
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Velocity = velocity;
		
		Health = GetNode<ProgressBar>("ProgressBar");
		Health.Value = health;
		
		if(health <= 0) {
			playerAlive = false;
			health = 0;
			GetTree().ChangeSceneToFile("res://game_over.tscn");
		}

		Movement();
		Animations();
		EnemyAttack();
		MoveAndSlide();
	}
	
	public void Animations() {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		//Estar parado
		if (!Input.IsAnythingPressed() && (Input.IsActionJustReleased("right") || Input.IsActionJustReleased("left") || Input.IsActionJustReleased("up") || Input.IsActionJustReleased("down"))) {
			animationPlayer.Play("Idle");
		}
		
		//Correr
		if (Input.IsActionPressed("right")) {
			animationPlayer.Play("Run");
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
		} else if(Input.IsActionPressed("left")) {
			animationPlayer.Play("Run");
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
		} else if(Input.IsActionPressed("up")) {
			animationPlayer.Play("Run");
		} else if(Input.IsActionPressed("down")) {
			animationPlayer.Play("Run");
		}
	}
	
	public void EnemyAttack() {
		if(en_range && en_attack_cooldown == true) {
			timer = GetNode<Timer>("Cooldown");
			health = health - damage;
			en_attack_cooldown = false;
			timer.Start();
		}
	}

	private void _on_player_hit_box_body_entered(Node2D body)
	{
		if(body.HasMethod("Enemy")) {
			en_range = true;
		}
	}
	
	private void _on_player_hit_box_body_exited(Node2D body)
	{
		if(body.HasMethod("Enemy")) {
			en_range = false;
		}
	}
	
	private void _on_cooldown_timeout()
	{
		en_attack_cooldown = true;
	}
	
	public void Player() { }

	public int seconds = 0;
	public int min = 0;
	public int Dseconds = 00;
	public int Dmin = 5;

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
		Label.Text = min.ToString() + ":" + seconds.ToString();
	}

	public void TimerReset() {
		seconds = Dseconds;
		min = Dmin;
	}
}
