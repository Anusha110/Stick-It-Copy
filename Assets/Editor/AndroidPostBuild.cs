#if UNITY_ANDROID

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
//using UnityEditor.iOS.Xcode;

public static class AndroidPostBuild
{
    public static string packageName = "com.proyuga.ibcplayerdevelop";
    public static string ApplicationName = PlayerSettings.productName;
    public static string javaProjectPath = "/src/main/java/"+ packageName.Replace(".","/") +"/";

    public const string AppFileName = "MyApp.java";
    public const string AppActivityFileName = "IBGamesActivity.java";
    public const string GradlePropertiesFile = "gradle.properties";


    [PostProcessBuild]
    public static void OnPostBuild(BuildTarget target, string pathToBuiltProject)
    {
        // string pathToBuiltProject = "/Users/office/Desktop/Mobile/Projects/ibcricket/ios/UnityLib_iOS";
        Debug.LogFormat("pathToBuiltProject '{0}'", pathToBuiltProject);

        if (target != BuildTarget.Android)
        {
            return;
        }
        FileUtil.CopyFileOrDirectory("/Users/office/Desktop/InstabugRequiredFiles/"+ AppFileName, 
                                     pathToBuiltProject+ "/"+ ApplicationName + javaProjectPath + AppFileName);
        FileUtil.CopyFileOrDirectory("/Users/office/Desktop/InstabugRequiredFiles/" + AppActivityFileName,
                                     pathToBuiltProject + "/" + ApplicationName + javaProjectPath + AppActivityFileName);
        FileUtil.CopyFileOrDirectory("/Users/office/Desktop/InstabugRequiredFiles/" + GradlePropertiesFile,
                                     pathToBuiltProject + "/" + ApplicationName +"/" + GradlePropertiesFile);
    }
}

#endif