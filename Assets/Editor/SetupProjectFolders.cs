using static UnityEditor.AssetDatabase;
using static System.IO.Directory;
using UnityEditor;

namespace UzGameDev
{
    public static class SetupProjectFolders
    {
        //===============================================================
        [MenuItem("Tools/Setup Project Folders")]
        public static void CreateProjectFolders()
        {
            var root = CreateDirectory("Assets/=GameContent=/");
            
            CreateDirectory(root + "Art");
            
            CreateDirectory(root + "Art/2D");
            CreateDirectory(root + "Art/2D/Sprites");
            CreateDirectory(root + "Art/2D/Textures");
            
            CreateDirectory(root + "Art/3D");
            CreateDirectory(root + "Art/3D/Materials");
            CreateDirectory(root + "Art/3D/Models");
            
            CreateDirectory(root + "Art/Animations");
            
            CreateDirectory(root + "Audio");
            CreateDirectory(root + "Audio/Musics");
            CreateDirectory(root + "Audio/Sounds");
            
            CreateDirectory(root + "Data");
            CreateDirectory(root + "Prefabs");
            CreateDirectory(root + "Scenes");
            CreateDirectory(root + "Scripts");
            CreateDirectory(root + "Temp");
            
            Refresh();
        }
        //===============================================================
    }
}