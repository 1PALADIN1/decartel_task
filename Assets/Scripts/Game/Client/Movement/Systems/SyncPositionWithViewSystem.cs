using Game.Core.Common;
using Leopotam.EcsLite;

namespace Game.Client
{
	public sealed class SyncPositionWithViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _positionFilter;

		private EcsPool<Position> _positionPool;
		private EcsPool<View> _viewPool;

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_positionFilter = world
				.Filter<Position>()
				.Inc<View>()
				.End();

			_positionPool = world.GetPool<Position>();
			_viewPool = world.GetPool<View>();
		}
		
		public void Run(EcsSystems systems)
		{
			foreach (var entity in _positionFilter)
			{
				ref var position = ref _positionPool.Get(entity);
				ref var view = ref _viewPool.Get(entity);
				view.Value.transform.position = position.Value;
			}
		}
	}
}