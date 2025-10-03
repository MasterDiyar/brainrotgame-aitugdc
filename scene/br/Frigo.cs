using Godot;
using System;
using System.Collections.Generic;

public partial class Frigo : BrainRoted
{
	Area2D area2D;
	
	private List<People> _targets = new();
	private Timer _attackTimer;
	
	private AnimatedSprite2D _icon;
	

	
	[Export] public float AttackRate = 1f;

	public override void _Ready()
	{
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		
		area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += EreaEntered;
		area2D.AreaExited += OnBodyExited;
		
		_attackTimer = GetNode<Timer>("Timer");
		_attackTimer.Timeout += DealDamage;
		_attackTimer.WaitTime = AttackRate;
	}

	void TimerCheck()
	{
		if (_targets.Count > 0)
		{
			_attackTimer.Start();
			_icon.Play();
		}
		else
		{
			_icon.Stop();
			_attackTimer.Stop();
		}
	}

	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/snezhok.tscn").Instantiate<Ljump>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*120+Vector2.Right*64;
		GetParent().AddChild(sharp);
		TimerCheck();
	}


	
	private void EreaEntered(Area2D area)
	{
		if (area.GetParent() is People enemy)
		{
			if (!_targets.Contains(enemy))
			{
				_targets.Add(enemy);
				TimerCheck();
				enemy.TreeExited += () => _targets.Remove(enemy);
			}
		}
	}

	private void OnBodyExited(Area2D body)
	{
		if (body.GetParent() is People enemy)
		{
			_targets.Remove(enemy);
		}
	}
}
