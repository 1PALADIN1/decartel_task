using Game.Core.Movement;
using Leopotam.EcsLite;

namespace Game.SceneConfig
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
                .Add(new MoveToDestinationSystem());
        }
    }
}