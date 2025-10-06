using Godot;
using System;

public partial class Menu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("start").Pressed += () =>
		{
			var sharp = GD.Load<PackedScene>("res://scene/game.tscn").Instantiate();
			GetParent().AddChild(sharp);
			QueueFree();
		};
	}

	
}
