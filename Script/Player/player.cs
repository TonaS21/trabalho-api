using Godot;
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
	public PackedScene SwordSlash = GD.Load<PackedScene>("res://tiro_attack.tscn");

	public int health = 100;
	public int damage = 0;

	public Vector2 mousePos;
	public Vector2 pos;
	public Vector2 direction;
	public bool pl_attack_cooldown = true;
	public int tirodamage = 100;

	public void Movement() {
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Velocity = velocity;
		
		Health = GetNode<ProgressBar>("Health");
		Health.Value = health;
		
		if(health <= 0) {
			playerAlive = false;
			health = 0;
			GetTree().ChangeSceneToFile("res://game_over.tscn");
		}

		Movement();
		Animations();
		EnemyAttack();
		TiroAttack();
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

	public void TiroAttack() {
		if(Input.IsActionJustPressed("attack") && pl_attack_cooldown == true) {
			var tiro = SwordSlash.Instantiate<tiro_attack>();
			mousePos = GetGlobalMousePosition();
			tiro.Position = Position + Position.DirectionTo(mousePos);
			tiro.direction = Position.DirectionTo(mousePos);
			tiro.velocity = 10;
			AddSibling(tiro);
			GD.Print(pos);
			tiro.Visible = true;
			pl_attack_cooldown = false;
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
		if(body.HasMethod("Enemy") || body.HasMethod("Enemy2")) {
			en_range = true;
		}
	}
	
	private void _on_player_hit_box_body_exited(Node2D body)
	{
		if(body.HasMethod("Enemy") || body.HasMethod("Enemy2")) {
			en_range = false;
		}
	}
	
	private void _on_cooldown_timeout()
	{
		en_attack_cooldown = true;
	}
	
	private void _on_tiro_cooldown_timeout()
	{
		pl_attack_cooldown = true;
	}
	
	public void Player() { }

	public override void _Ready() { }
}

