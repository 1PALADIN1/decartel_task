using Game.Core.Doors;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Client
{
	public sealed class DoorOpenAnimationSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _doorFilter;

		private EcsPool<Door> _doorPool;
		private EcsPool<View> _viewPool;
		private EcsPool<DoorAnimation> _doorAnimationPool;

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_doorFilter = world
				.Filter<Door>()
				.Inc<View>()
				.Inc<DoorAnimation>()
				.End();

			_doorPool = world.GetPool<Door>();
			_viewPool = world.GetPool<View>();
			_doorAnimationPool = world.GetPool<DoorAnimation>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _doorFilter)
			{
				ref var door = ref _doorPool.Get(entity);
				ref var view = ref _viewPool.Get(entity);
				ref var doorAnimation = ref _doorAnimationPool.Get(entity);
				
				var currentPosition = view.Value.transform.position;
				currentPosition.y = Mathf.Lerp(doorAnimation.FromY, doorAnimation.ToY, 1f - door.CurrentOpenValue);
				view.Value.transform.position = currentPosition;
			}
		}
	}
}