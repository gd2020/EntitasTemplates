using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

[InitializeOnLoad]
public class EntitasUnityTemplate
{
    public class Config
    {
        public const string PATH = "Assets/Editor/EntitasUnityTemplateFiles/";

        public const string ENTITAS_COMPONENT_FILE_NAME = "EntitasComponent.txt";
        public const string INITIALIZE_SYSTEM_FILE_NAME = "InitializeSystem.txt";
        public const string REACTIVE_SYSTEM_FILE_NAME   = "ReactiveSystem.txt";
        public const string CLEANUP_SYSTEM_FILE_NAME    = "CleanupSystem.txt";
        public const string EXECUTE_SYSTEM_FILE_NAME    = "ExecuteSystem.txt";

    }

    public class DoCreateScriptAsset : EndNameEditAction
    {
        public override void Action(int _instance_id, string _path_name, string _resource_file)
        {
            var text = File.ReadAllText(_resource_file);
            var class_name = Path.GetFileNameWithoutExtension(_path_name);
            class_name = class_name.Replace(" ", "");
            text = text.Replace("#SCRIPTNAME#", class_name);

            var encoding = new UTF8Encoding(true, false);
            File.WriteAllText(_path_name, text, encoding);

            AssetDatabase.ImportAsset(_path_name);
            var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(_path_name);
            ProjectWindowUtil.ShowCreatedAsset(asset);
        }
    }
    
    static void CreateEntitasScriptAsset( string _file_name )
    {
        string    cs_file_path  = Config.PATH + _file_name;
        Texture2D cs_icon       = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

        string new_file_name = "New"+ _file_name.Replace(".txt", ".cs");

        var end_name_action = ScriptableObject.CreateInstance<DoCreateScriptAsset>();
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, end_name_action, new_file_name, cs_icon, cs_file_path);
    }


    [MenuItem("Assets/Create/Entitas/Entitas Component")]
    static void CreateEntitasComponent()
    {
        CreateEntitasScriptAsset(Config.ENTITAS_COMPONENT_FILE_NAME);
    }
    [MenuItem("Assets/Create/Entitas/Initialize System")]
    static void CreateInitializeSystem()
    {
        CreateEntitasScriptAsset(Config.INITIALIZE_SYSTEM_FILE_NAME);
    }
    [MenuItem("Assets/Create/Entitas/Reactive System")]
    static void CreateReactiveSystem()
    {
        CreateEntitasScriptAsset(Config.REACTIVE_SYSTEM_FILE_NAME);
    }
    [MenuItem("Assets/Create/Entitas/Execute System")]
    static void CreateExecuteSystem()
    {
        CreateEntitasScriptAsset(Config.EXECUTE_SYSTEM_FILE_NAME);
    }
    [MenuItem("Assets/Create/Entitas/Cleanup System")]
    static void CreateCleanupSystem()
    {
        CreateEntitasScriptAsset(Config.CLEANUP_SYSTEM_FILE_NAME);
    }
}
