using UnityEditor;

public class EWStaticData : EditorWindow
{
    [MenuItem("PopoteTools/StaticData")]
    public static void OnDisplayWindonw() { 
        EWStaticData window = GetWindow<EWStaticData>();
        window.name = "Static data";
    }

    public void OnGUI()
    {
        if (!EditorApplication.isPlaying) {
            EditorGUILayout.LabelField(" Not In Play Mode");
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(" Current Working building:");
            EditorGUILayout.LabelField(StaticData.WorkingBuildings.Count.ToString());
            EditorGUILayout.EndHorizontal();
            foreach (var building in StaticData.WorkingBuildings) {
                EditorGUILayout.LabelField(building.ToString());
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(" Current Citiznes:");
            EditorGUILayout.LabelField(StaticData.Citizens.Count.ToString());
            EditorGUILayout.EndHorizontal();
            foreach (var citizen in StaticData.Citizens) {
                EditorGUILayout.LabelField(citizen.Name.ToString());
            }
        }
    }
    
}
