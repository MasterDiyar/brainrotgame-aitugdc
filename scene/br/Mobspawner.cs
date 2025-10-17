using Godot;
using System;
using System.Threading.Tasks;

public partial class Mobspawner : Node2D
{
	roundVizov roundVizov;
    
    [Export] public PackedScene[] MobScenes = Array.Empty<PackedScene>();

    [Export] public NodePath SpawnParentPath;
    private Node spawnParent;

    [Export] public float MinIntervalSeconds = 20f;
    [Export] public float MaxIntervalSeconds = 40f;
    [Export] public float MinIntervalLimit = 16f; // ниже этого не опускаем

    [Export] public int BaseMinPerWave = 1;
    [Export] public int BaseMaxPerWave = 6;
    [Export] public int WaveGrowth = 1; 

    [Export] public float SpawnXMin = 1700f;
    [Export] public float SpawnXMax = 1800f;
    [Export] public int[] SpawnYPoints = new int[] { 286, 430, 588, 733 };

    [Export] public float StaggerBetweenSpawns = 0.0f;

    private RandomNumberGenerator rng = new RandomNumberGenerator();
    private Timer waveTimer;

    public int waveLevel = 1; 
    private float currentMinInterval;
    private float currentMaxInterval;

    public override void _Ready()
    {
        roundVizov = GetNode<roundVizov>("Label");
        rng.Randomize();
        spawnParent = GetNodeOrNull(SpawnParentPath) ?? this;

        currentMinInterval = MinIntervalSeconds;
        currentMaxInterval = MaxIntervalSeconds;

        waveTimer = new Timer();
        AddChild(waveTimer);
        waveTimer.OneShot = true;
        waveTimer.Timeout += OnWaveTimerTimeout;

        StartNextWaveTimer();
    }

    private void StartNextWaveTimer()
    {
        float next = rng.RandfRange(currentMinInterval, currentMaxInterval);
        waveTimer.WaitTime = next;
        waveTimer.Start();
        GD.Print($"[SpawnManager] Wave {waveLevel} in {next:F1}s");
    }

    private async void OnWaveTimerTimeout()
    {
        await SpawnWave();
        waveLevel++;
        currentMinInterval = Mathf.Max(currentMinInterval * 0.95f, MinIntervalLimit);
        currentMaxInterval = Mathf.Max(currentMaxInterval * 0.95f, MinIntervalLimit + 2);

        StartNextWaveTimer();
    }

    private async Task SpawnWave()
    {
        roundVizov.Call(waveLevel.ToString());
        if (MobScenes == null || MobScenes.Length == 0)
        {
            GD.PrintErr("[SpawnManager] Нет сцен мобов!");
            return;
        }

        int minCount = BaseMinPerWave + (waveLevel / 3);
        int maxCount = BaseMaxPerWave + (waveLevel / 2);
        int count = rng.RandiRange(minCount, maxCount);

        GD.Print($"[SpawnManager] === Wave {waveLevel} | Spawning {count} mobs ===");

        for (int i = 0; i < count; i++)
        {
            var scene = GetWeightedMobScene();
            if (scene == null) continue;

            var inst = scene.Instantiate<People>();
            
            float x = rng.RandfRange(SpawnXMin, SpawnXMax);
            int y = SpawnYPoints[rng.RandiRange(0, SpawnYPoints.Length - 1)];
            inst.Position = new Vector2(x, y);
            

            spawnParent.AddChild(inst);

            if (StaggerBetweenSpawns > 0f && i < count - 1)
                await ToSignal(GetTree().CreateTimer(StaggerBetweenSpawns), Timer.SignalName.Timeout);
        }
    }
    private PackedScene GetWeightedMobScene()
    {
        if (MobScenes.Length == 1)
            return MobScenes[0];

        float bias = Mathf.Clamp(waveLevel / 80f, 0f, 1f);
        float indexF = Mathf.Pow(rng.Randf(), 1f - bias); // чем больше bias, тем ближе к 1
        int index = Mathf.Clamp(Mathf.FloorToInt(indexF * MobScenes.Length), 0, MobScenes.Length - 1);
        if (waveLevel < 8 && index >=5)
            return MobScenes[index-3];
        return MobScenes[index];
    }
}