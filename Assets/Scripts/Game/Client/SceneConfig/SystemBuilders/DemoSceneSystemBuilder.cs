using Game.Core.Doors;
using Game.Core.Movement;
using Leopotam.EcsLite;

namespace Game.Client.SceneConfig
{
    public sealed class DemoSceneSystemBuilder : BaseSceneSystemBuilder
    {
        public override void Build(EcsSystems ecsSystems)
        {
            CreateGameplaySystems(ecsSystems);
        }

        private void CreateGameplaySystems(EcsSystems ecsSystems)
        {
            ecsSystems
                .Add(new MoveToDestinationSystem())
                //doors
                .Add(new FloorButtonPressedSystem())
                .Add(new FloorButtonReleasedSystem());
        }
    }
}