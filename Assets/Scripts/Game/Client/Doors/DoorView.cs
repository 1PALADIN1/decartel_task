using Game.Core.Doors;
using UnityEngine;

namespace Game.Client
{
	public class DoorView : BaseEntityView
	{
		[SerializeField] private DoorColor _doorColor;
		[SerializeField, Min(0f)] private float _doorOpenSpeed;

		[Header("Animation")]
		[SerializeField] private float _maxY;
		[SerializeField] private float _minY;
		
		protected override void OnInitialized()
		{
			var entity = World.NewEntity();
			ref var door = ref World.GetPool<Door>().Add(entity);
			door.Color = _doorColor;
			door.OpenSpeed = _doorOpenSpeed;

			ref var view = ref World.GetPool<View>().Add(entity);
			view.Value = gameObject;

			ref var doorAnimation = ref World.GetPool<OpenCloseAnimation>().Add(entity);
			doorAnimation.MaxY = _maxY;
			doorAnimation.MinY = _minY;
		}
	}
}