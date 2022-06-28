using System.Collections.Generic;
using Game.Core.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Core.Doors
{
	/// <summary>
	/// Система для открывания дверей, если нажата соответстующая кнопка
	/// </summary>
	public sealed class DoorOpenSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _doorFilter;
		private EcsFilter _buttonPressedFilter;
		private EcsFilter _deltaTimeFilter;
		
		private EcsPool<Door> _doorPool;
		private EcsPool<FloorButtonPressed> _buttonPressedPool;
		private EcsPool<DeltaTime> _deltaTimePool;

		private HashSet<DoorColor> _openingDoorColors; 

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_doorFilter = world
				.Filter<Door>()
				.End();

			_buttonPressedFilter = world
				.Filter<FloorButtonPressed>()
				.End();

			_deltaTimeFilter = world
				.Filter<DeltaTime>()
				.End();

			_doorPool = world.GetPool<Door>();
			_buttonPressedPool = world.GetPool<FloorButtonPressed>();
			_deltaTimePool = world.GetPool<DeltaTime>();

			_openingDoorColors = new HashSet<DoorColor>();
		}

		public void Run(EcsSystems systems)
		{
			_openingDoorColors.Clear();
			
			var dt = 0f;
			foreach (var deltaTimeEntity in _deltaTimeFilter)
			{
				ref var deltaTime = ref _deltaTimePool.Get(deltaTimeEntity);
				dt = deltaTime.Value;
				break;
			}
			
			foreach (var buttonEntity in _buttonPressedFilter)
			{
				ref var pressedButton = ref _buttonPressedPool.Get(buttonEntity);
				if (!_openingDoorColors.Contains(pressedButton.Color))
					_openingDoorColors.Add(pressedButton.Color);
			}

			foreach (var doorEntity in _doorFilter)
			{
				ref var door = ref _doorPool.Get(doorEntity);
				if (_openingDoorColors.Contains(door.Color))
					door.CurrentOpenValue = Mathf.Max(0f, door.CurrentOpenValue - door.OpenSpeed * dt);
			}
		}
	}
}