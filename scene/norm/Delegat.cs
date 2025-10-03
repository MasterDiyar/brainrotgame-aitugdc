using Godot;
using System;

public partial class Delegat : People
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Position += Vector2.Left * Speed*(float)delta;
	}
}
