using Godot;
using System;

public partial class GameManager : Node2D
{
	public float Money;
	public Label MoneyLabel, info;
	bool IsJoystickConnected()
	{
		// Получаем список всех подключённых устройств ввода
		var connected = Input.GetConnectedJoypads();
		return connected.Count > 0;
	}
	public override void _Ready()
	{
		Money = 110;
		MoneyLabel = GetNode<Label>("PurseCounter");
		info = GetNode<Label>("cnoth/ColorRect/Label2");
		info.Text = (IsJoystickConnected())? "Controls:\n \u2b06-up \u2b07-down \u2b05-left \u27a1-right \n X-Choose O-exit \u2612-delete " : 
		"Controls:\nW-up A-left S-down D-right\nQ-choose E-exit R-delete";
	}
	public override void _Process(double delta)
	{
		Money += (float)delta;
		MoneyLabel.Text = "Prompt: "+ ((int)Money).ToString();
	}

	public void leaveGame()
	{
		var looser = GD.Load<PackedScene>("res://scene/loose.tscn").Instantiate();
		looser.GetNode<Label>("Label").Text = GetNode<Mobspawner>("mobspawner").waveLevel.ToString()+" levels alived! Molodec";
		GetParent().AddChild(looser);
		
		GetParent().RemoveChild(this);
	}
}
