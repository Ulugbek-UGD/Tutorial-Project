using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class FullScreenSprite : MonoBehaviour
    {
        private Camera mainCamera;
        private SpriteRenderer spriteRenderer;
        
        //============================================================
        private void Awake()
        {
            mainCamera = Camera.main;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        //============================================================
        private void Update()
        {
            transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
            
            var worldScreenHeight = mainCamera.orthographicSize * 2f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
            
            var spriteSize  = spriteRenderer.sprite.bounds.size;
            var scale = Vector3.one;
            scale.x = worldScreenWidth / spriteSize.x;
            scale.y = worldScreenHeight / spriteSize.y;
            
            transform.localScale = scale;
        }
        //============================================================
    }
}