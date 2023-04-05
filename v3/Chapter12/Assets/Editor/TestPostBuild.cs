using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class TestPostBuild
{
   [PostProcessBuild]
   public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
   {
      Debug.Log($"Build location: {pathToBuiltProject}");
   }
}
