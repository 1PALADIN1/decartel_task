using Game.Core.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Client
{
	public sealed class DeltaTimeRefreshSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _deltaTimeFilter;

		private EcsPool<DeltaTime> _deltaTimePool;

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_deltaTimeFilter = world
				.Filter<DeltaTime>()
				.End();
			
			_deltaTimePool = world.GetPool<DeltaTime>();
		}
		
		public void Run(EcsSystems systems)
		{
			foreach (var entity in _deltaTimeFilter)
			{
				ref var deltaTime = ref _deltaTimePool.Get(entity);
				deltaTime.Value = Time.deltaTime;
			}
		}
	}
}