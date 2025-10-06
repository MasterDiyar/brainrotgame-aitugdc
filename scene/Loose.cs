using Godot;
using System;

public partial class Loose : Node2D
{
	
	public override void _Ready()
	{
		GetNode<Button>("Button").Pressed += () =>
		{
			var sharp = GD.Load<PackedScene>("res://scene/menu.tscn").Instantiate();
			GetParent().AddChild(sharp);
			
			QueueFree();
		};
	}

	
}
