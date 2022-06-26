using Game.Core.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Core.Movement
{
    public sealed class MoveToDestinationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _movementFilter;

        private EcsPool<Destination> _destinationPool;
        private EcsPool<Position> _positionPool;
        private EcsPool<Movement> _movementPool;
        private EcsPool<DeltaTime> _deltaTimePool;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            _movementFilter = world
                .Filter<Destination>()
                .Inc<Position>()
                .Inc<Movement>()
                .End();
            
            _destinationPool = world.GetPool<Destination>();
            _positionPool = world.GetPool<Position>();
            _movementPool = world.GetPool<Movement>();
            _deltaTimePool = world.GetPool<DeltaTime>();
        }
        
        public void Run(EcsSystems systems)
        {
            foreach (var entity in _movementFilter)
            {
                ref var destination = ref _destinationPool.Get(entity);
                ref var position = ref _positionPool.Get(entity);
                ref var movement = ref _movementPool.Get(entity);
                
                if (Mathf.Abs(destination.Value.x - position.Value.x) <= movement.StoppingDistance
                    && Mathf.Abs(destination.Value.y - position.Value.y) <= movement.StoppingDistance)
                {
                    //объект у цели
                    _destinationPool.Del(entity);
                    continue;
                }

                ref var deltaTime = ref _deltaTimePool.Get(entity);
                var movementOffset = (destination.Value - position.Value).normalized 
                                    * (movement.Speed * deltaTime.Value);

                position.Value += movementOffset;
            }
        }
    }
}