using Godot;
using System;

public partial class GameManager : Node2D
{
	public float Money;
	public Label MoneyLabel;
	public override void _Ready()
	{
		Money = 500;
		MoneyLabel = GetNode<Label>("PurseCounter");
	}
	public override void _Process(double delta)
	{
		MoneyLabel.Text = "Prompt: "+Money.ToString();
	}
}
