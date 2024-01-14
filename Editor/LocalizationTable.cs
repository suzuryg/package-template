using UnityEngine;

namespace Suzuryg.PackageName
{
    // [CreateAssetMenu(menuName = "PackageName/LocalizationTable")]
    internal class LocalizationTable : ScriptableObject
    {
        [Header("MainWindow_UI")]
        public string Execute = "Execute";
        public string Cancel = "Cancel";

        [Header("MainWindow_Message")]
        public string PleaseSelectSceneObject = "Please select an object on the scene (you cannot select an asset).";
        public string Confirm = "Do you want to execute?";
    }
}
