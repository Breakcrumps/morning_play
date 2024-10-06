public partial class SceneTransition : Area2D
{
  [Export(PropertyHint.File, "*.tscn")] private string _scenePath;

  [Export] private int _spawnIndex;

  public override void _Ready()
  {
    BodyEntered += TransitionScene;
  }

  private void TransitionScene(Node2D body)
  {
    if (body is not Player)
      return;

    var currentScene = GetOwner<Scene>();
    var nextScene = ResourceLoader.Load<PackedScene>(_scenePath).Instantiate<Scene>();
    nextScene.PlayerSpawnIndex = _spawnIndex;
    GetTree().Root.CallDeferred("add_child", nextScene);
    GetTree().Root.CallDeferred("remove_child", currentScene);
  }
}