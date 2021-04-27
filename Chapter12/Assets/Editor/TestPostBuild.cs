using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class TestPostBuild
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
    {
        Debug.Log("build location: " + pathToBuildProject);
    }
}