using Game.Core.Doors;
using Game.Core.Movement;
using Leopotam.EcsLite;

namespace Game.Client.SceneConfig
{
    public sealed class DemoSceneSystemBuilder : BaseSceneSystemBuilder
    {
        public override void Build(EcsSystems ecsSystems)
        {
            CreateCoreSystems(ecsSystems);
            CreateClientSystems(ecsSystems);
        }

        private void CreateCoreSystems(EcsSystems ecsSystems)
        {
            ecsSystems
                //movement
                .Add(new MoveToDestinationSystem())
                //doors
                .Add(new FloorButtonPressedSystem())
                .Add(new FloorButtonReleasedSystem())
                .Add(new DoorSetupSystem())
                .Add(new DoorOpenSystem());
        }

        private void CreateClientSystems(EcsSystems ecsSystems)
        {
            ecsSystems
                //common
                .Add(new DeltaTimeRefreshSystem())
                //player
                .Add(new PlayerControlSystem())
                //movement
                .Add(new SyncPositionWithViewSystem())
                //doors
                .Add(new DoorOpenAnimationSystem())
                .Add(new ButtonPressedAnimation());
        }
    }
}