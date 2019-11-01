using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FindReferencesInProject
{
    public class FindReferencesInProject : EditorWindow
    {
        private static Dictionary<AssetData, List<AssetData>> Results = new Dictionary<AssetData, List<AssetData>>();
        private static Dictionary<AssetData, bool> Foldouts = new Dictionary<AssetData, bool>();
        private Vector2 ScrollPosition = Vector2.zero;

        [MenuItem("Assets/Find References In Project", true)]
        static bool IsEnabled()
        {
            return Selection.objects.Any();
        }

        [MenuItem("Assets/Find References In Project", false, 25)]
        static void Search()
        {
            Results.Clear();
            Foldouts.Clear();

            foreach (var target in AssetDatabase.FindAssets("t:Scene t:Prefab").Select(AssetData.CreateByGuid))
            {
                foreach (var referent in AssetDatabase.GetDependencies(target.Path).Select(AssetData.CreateByPath))
                {
                    if (target.Equals(referent)) { continue; }

                    foreach (var selected in Selection.objects.Select(AssetData.CreateByObject))
                    {
                        if (referent.Equals(selected))
                        {
                            Results.AddSafety(referent, new List<AssetData>());
                            Results[referent].Add(target);
                        }
                    }
                }
            }

            GetWindow<FindReferencesInProject>();
        }

        void OnGUI()
        {
            this.ScrollPosition = GUILayout.BeginScrollView(this.ScrollPosition);

            foreach (var referent in Results.Keys.OrderBy(key => key.Name).ToList())
            {
                Foldouts.AddSafety(referent, true);

                if (Foldouts[referent] = EditorGUILayout.Foldout(Foldouts[referent], referent.Name))
                {
                    foreach (var target in Results[referent])
                    {
                        var iconSize = EditorGUIUtility.GetIconSize();
                        EditorGUIUtility.SetIconSize(Vector2.one * 16);

                        var obj = target.ToObject();
                        var content = new GUIContent(target.Name, EditorGUIUtility.ObjectContent(obj, obj.GetType()).image);

                        if (GUILayout.Button(content, "Label"))
                        {
                            Selection.objects = new[] { obj };
                        }

                        EditorGUIUtility.SetIconSize(iconSize);
                    }
                }
            }

            GUILayout.EndScrollView();
        }
    }

    public class AssetData
    {
        public string Name { get; }
        public string Path { get; }
        public string Guid { get; }

        public AssetData(string name, string path, string guid)
        {
            this.Name = name;
            this.Path = path;
            this.Guid = guid;
        }

        public static AssetData CreateByObject(Object obj)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            var guid = AssetDatabase.AssetPathToGUID(path);
            var name = obj.name;
            return new AssetData(name, path, guid);
        }

        public static AssetData CreateByPath(string path)
        {
            var guid = AssetDatabase.AssetPathToGUID(path);
            var name = AssetDatabase.LoadMainAssetAtPath(path).name;
            return new AssetData(name, path, guid);
        }

        public static AssetData CreateByGuid(string guid)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var name = AssetDatabase.LoadMainAssetAtPath(path).name;
            return new AssetData(name, path, guid);
        }

        public Object ToObject()
        {
            return AssetDatabase.LoadAssetAtPath<Object>(this.Path);
        }

        public override bool Equals(object obj)
        {
            var other = obj as AssetData;
            return other != null && this.Guid == other.Guid;
        }

        public override int GetHashCode()
        {
            return this.Guid.GetHashCode();
        }
    }

    public static class DictionaryExtension
    {
        public static void AddSafety<K, V>(this Dictionary<K, V> self, K key, V value)
        {
            if (!self.ContainsKey(key))
            {
                self.Add(key, value);
            }
        }
    }
}