using Godot;
using System;

public partial class Lavaka : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 320;
	[Export] public float AttackRate = 0.2f;
	private int line = 0;
	private Vector2[] angleNorm = [new Vector2(0.866f ,0.5f), new Vector2(1, 0), new Vector2(0.866f ,-0.5f)];
	public override void _Ready()
	{
		base._Ready();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_icon.Play();
		
		_attackTimer.Timeout += DealDamage;
		_attackTimer.WaitTime = AttackRate;
	}

	public void DealDamage()
	{
		line+= (line==2) ? -2: 1;
		GD.Print(line);
		var sharp = GD.Load<PackedScene>("res://scene/br/meteor.tscn").Instantiate<Meteor>();
		sharp.Position = _icon.GlobalPosition+Vector2.Right*128;
		sharp.NormAngle = angleNorm[line];
		GetParent().AddChild(sharp);
		TimerCheck();
	}
}
