using Godot;
using System;
using System.Collections.Generic;

public partial class Cnoth : Control
{
	private const int SpriteCount = 9;

	private readonly string[] SpriteNames = {
		"Ram", "Ram2", "Ram3", "Ram4", "Ram5", "Ram6", "Ram7", "Ram8", "Ram9"
	};

	private Sprite2D[] sprites = new Sprite2D[SpriteCount];
	private ShaderMaterial[] materials = new ShaderMaterial[SpriteCount];

	[Export]private float[] rechargeTimes = {
		1.5f, 2.0f, 3.0f, 2.5f, 1.2f, 4.0f, 3.5f, 2.8f, 5.0f
	};

	private float[] progress = new float[SpriteCount];
	private float[] timers = new float[SpriteCount];

	public override void _Ready()
	{
		for (int i = 0; i < SpriteCount; i++)
		{
			sprites[i] = GetNode<Sprite2D>(SpriteNames[i]);
			materials[i] = (ShaderMaterial)sprites[i].Material.Duplicate();
			sprites[i].Material = materials[i];
			progress[i] = 0f;
			timers[i] = 0f;
			StartRecharge(i);
		}
	}

	public override void _Process(double delta)
	{
		for (int i = 0; i < SpriteCount; i++)
		{
			if (progress[i] > 0f)
			{
				timers[i] -= (float)delta;
				progress[i] = Mathf.Clamp(timers[i] / rechargeTimes[i], 0f, 1f);
				materials[i].SetShaderParameter("recharge_progress", progress[i]);
			}
		}
	}
	public void StartRecharge(int index)
	{
		if (index < 0 || index >= SpriteCount) return;
		timers[index] = rechargeTimes[index];
		progress[index] = 1f;
		materials[index].SetShaderParameter("recharge_progress", 1f);
	}
	public bool IsRecharged(int index)
	{
		if (index < 0 || index >= SpriteCount) return false;
		return progress[index] <= 0f;
	}
}
