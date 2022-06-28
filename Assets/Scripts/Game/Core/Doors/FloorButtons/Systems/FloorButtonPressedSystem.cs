using Game.Core.Common;
using Leopotam.EcsLite;

namespace Game.Core.Doors
{
	public sealed class FloorButtonPressedSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _playerFilter;
		private EcsFilter _floorButtonFilter;

		private EcsPool<FloorButton> _floorButtonPool;
		private EcsPool<Position> _positionPool;
		private EcsPool<FloorButtonPressed> _buttonPressedPool;

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_playerFilter = world
				.Filter<Player>()
				.Inc<Position>()
				.End();

			_floorButtonFilter = world
				.Filter<FloorButton>()
				.Inc<Position>()
				.Exc<FloorButtonPressed>()
				.End();

			_floorButtonPool = world.GetPool<FloorButton>();
			_positionPool = world.GetPool<Position>();
			_buttonPressedPool = world.GetPool<FloorButtonPressed>();
		}
		
		public void Run(EcsSystems systems)
		{
			foreach (var playerEntity in _playerFilter)
			{
				ref var playerPosition = ref _positionPool.Get(playerEntity);

				foreach (var buttonEntity in _floorButtonFilter)
				{
					ref var buttonPosition = ref _positionPool.Get(buttonEntity);
					ref var floorButton = ref _floorButtonPool.Get(buttonEntity);
					
					if ((playerPosition.Value - buttonPosition.Value).sqrMagnitude > floorButton.Radius * floorButton.Radius)
						continue;

					//кнопка нажата
					ref var buttonPressed = ref _buttonPressedPool.Add(buttonEntity);
					buttonPressed.Color = floorButton.Color;
				}
			}
		}
	}
}