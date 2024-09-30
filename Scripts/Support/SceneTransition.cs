public partial class SceneTransition : Area2D {

  [Export(PropertyHint.File, "*.tscn")]
  private string _scenePath;

  public override void _Ready() {
    BodyEntered += TransitionScene;
  }

  private void TransitionScene(Node2D body) {

    if (body is not Player)
      return;

    var scene = ResourceLoader.Load<PackedScene>(_scenePath);
    GetTree().CallDeferred("change_scene_to_packed", scene);
    
  }

}