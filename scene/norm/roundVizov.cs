using Godot;
using System;

public partial class roundVizov : Label
{
	private float Speed = 10, uskorenie = 0.5f;
	bool oneTime = false, stop = false;

public override void _Process(double delta)
	{
		if (stop) return;
		Speed += uskorenie;
		Position += Vector2.Right*Speed;
		if (Position.X > 800 && !oneTime)
		{
			oneTime = true;
			Speed = 0;
			uskorenie = 0.3f;
		}
		
		if (Position.X > 2400) stop = true;
	}

	public void Call(string text)
	{
		this.Text = text+" round";
		stop = false;
		oneTime = false;
		Position = new Vector2(-600, 512);
	}
}
