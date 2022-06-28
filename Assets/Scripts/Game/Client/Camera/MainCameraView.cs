using UnityEngine;

namespace Game.Client
{
	public class MainCameraView : BaseEntityView
	{
		[SerializeField] private Camera _mainCamera;
		
		protected override void OnInitialized()
		{
			var entity = World.NewEntity();
			ref var mainCamera = ref World.GetPool<MainCamera>().Add(entity);
			mainCamera.Camera = _mainCamera;
		}
	}
}