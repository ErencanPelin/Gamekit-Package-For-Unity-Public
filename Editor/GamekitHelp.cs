using UnityEditor;

namespace Gamekit2D.Editor
{
    public class GamekitHelp : EditorWindow
    {
        [MenuItem ("Eren's Gamekit/Gamekit Manual", false, -100)]
        public static void ShowWindow() 
        {
            System.Diagnostics.Process.Start("https://erensoftworks.wordpress.com/documentation/");
        }
        
        [MenuItem ("Eren's Gamekit/Gamekit Support Discord", false, -100)]
        public static void ShowDiscord() 
        {
            System.Diagnostics.Process.Start("https://discord.com/invite/K4fQ6s82Q4");
        }
    }
}