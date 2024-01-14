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
	public int damage;

	public Vector2 mousePos;
	public Vector2 pos;
	public Vector2 direction;
	public bool pl_tiroattack_cooldown = true;
	public int tirodamage = 10;

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
		
		Movement();
		Animations();
		EnemyAttack();
		TiroAttack();
		MoveAndSlide();
	}
	
	public void Animations() {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		//Estar parado
		if (!Input.IsAnythingPressed()) {
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
		if(Input.IsActionJustPressed("tiroattack") && pl_tiroattack_cooldown == true) {
			var tiro = SwordSlash.Instantiate<tiro_attack>();
			mousePos = GetGlobalMousePosition();
			tiro.Position = Position + Position.DirectionTo(mousePos);
			tiro.direction = Position.DirectionTo(mousePos);
			tiro.velocity = 10;
			AddSibling(tiro);
			GD.Print(pos);
			tiro.Visible = true;
			pl_tiroattack_cooldown = false;
		}
	}

	public void EnemyAttack() {
		if(en_range && en_attack_cooldown == true) {
			timer = GetNode<Timer>("Cooldown");
			if(health - damage > 0) {
				health = health - damage;
				en_attack_cooldown = false;
				timer.Start();
			} else {
				health = 0;
				playerAlive = false;
				GetTree().ChangeSceneToFile("res://game_over.tscn");
			}
		}
	}

	private void _on_player_hit_box_body_entered(Node2D body)
	{

		if(body.Name == "Enemy") {
			damage = 10;
			en_range = true;
		} else if (body.Name == "Enemy2") {
			damage = 20;
			en_range = true;
		}
	}
	
	private void _on_player_hit_box_body_exited(Node2D body)
	{
		if(body.Name == "Enemy" || body.Name == "Enemy2") {
			en_range = false;
		}
	}
	
	private void _on_cooldown_timeout()
	{
		en_attack_cooldown = true;
	}
	
	private void _on_tiro_cooldown_timeout()
	{
		pl_tiroattack_cooldown = true;
	}
	
	public override void _Ready() { }
}

