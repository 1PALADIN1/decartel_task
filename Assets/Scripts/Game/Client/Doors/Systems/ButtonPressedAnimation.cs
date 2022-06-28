using Game.Core.Doors;
using Leopotam.EcsLite;

namespace Game.Client
{
	public sealed class ButtonPressedAnimation : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _animationFilter;
		
		private EcsPool<View> _viewPool;
		private EcsPool<OpenCloseAnimation> _animationPool;
		private EcsPool<FloorButtonPressed> _floorButtonPressedPool;

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_animationFilter = world
				.Filter<FloorButton>()
				.Inc<View>()
				.Inc<OpenCloseAnimation>()
				.End();
			
			_viewPool = world.GetPool<View>();
			_animationPool = world.GetPool<OpenCloseAnimation>();
			_floorButtonPressedPool = world.GetPool<FloorButtonPressed>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _animationFilter)
			{
				var isButtonPressed = _floorButtonPressedPool.Has(entity);

				ref var view = ref _viewPool.Get(entity);
				ref var animation = ref _animationPool.Get(entity);

				var currentPosition = view.Value.transform.position;
				currentPosition.y = isButtonPressed ? animation.MinY : animation.MaxY;
				view.Value.transform.position = currentPosition;
			}
		}
	}
}