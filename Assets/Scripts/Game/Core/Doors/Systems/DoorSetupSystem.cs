using Leopotam.EcsLite;

namespace Game.Core.Doors
{
	public sealed class DoorSetupSystem : IEcsInitSystem
	{
		private EcsFilter _doorFilter;
		
		private EcsPool<Door> _doorPool;
		
		public void Init(EcsSystems systems)
		{
			foreach (var entity in _doorFilter)
			{
				ref var door = ref _doorPool.Get(entity);
				door.CurrentOpenValue = 1f;
			}
		}
	}
}