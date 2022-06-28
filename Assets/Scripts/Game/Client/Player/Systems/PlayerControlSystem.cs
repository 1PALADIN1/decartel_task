using Game.Core.Common;
using Game.Core.Movement;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Client
{
	public sealed class PlayerControlSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _playerFilter;
		private EcsFilter _mainCameraFilter;
		
		private EcsPool<Destination> _destinationPool;
		private EcsPool<Position> _positionPool;
		private EcsPool<MainCamera> _mainCameraPool;

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();
			
			_playerFilter = world
				.Filter<Player>()
				.End();

			_mainCameraFilter = world
				.Filter<MainCamera>()
				.End();

			_destinationPool = world.GetPool<Destination>();
			_positionPool = world.GetPool<Position>();
			_mainCameraPool = world.GetPool<MainCamera>();
		}
		
		public void Run(EcsSystems systems)
		{
			if (!Input.GetMouseButtonDown(0))
				return;

			RaycastHit? raycastHit = null;
			foreach (var entity in _mainCameraFilter)
			{
				ref var mainCamera = ref _mainCameraPool.Get(entity);
				var ray = mainCamera.Camera.ScreenPointToRay(Input.mousePosition);
				if (!Physics.Raycast(ray, out var result))
					continue;

				raycastHit = result;
			}
			
			if (!raycastHit.HasValue)
				return;

			foreach (var entity in _playerFilter)
			{
				ref var playerPosition = ref _positionPool.Get(entity);
				var targetPosition = new Vector3(raycastHit.Value.point.x, 
					playerPosition.Value.y, raycastHit.Value.point.z);

				if (_destinationPool.Has(entity))
				{
					ref var destination = ref _destinationPool.Get(entity);
					destination.Value = targetPosition;
					continue;
				}
				
				ref var newDestination = ref _destinationPool.Add(entity);
				newDestination.Value = targetPosition;
			}
		}
	}
}