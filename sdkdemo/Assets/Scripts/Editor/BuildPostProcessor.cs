using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using System.IO;

public class BuildPostProcessor {
	private static PBXProject _project;
  	private static string _path;
  	private static string _projectPath;
	private static string _target;

	private static void OpenProject() {
		_projectPath = _path + "/Unity-iPhone.xcodeproj/project.pbxproj";

		_project = new PBXProject ();
		_project.ReadFromFile (_projectPath);

		_target = _project.TargetGuidByName("Unity-iPhone");
  	}

	private static void CloseProject() {
    	File.WriteAllText(_projectPath, _project.WriteToString ());
  	}

	internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
	{
		if (Directory.Exists(dstPath))
			Directory.Delete(dstPath);
		if (File.Exists(dstPath))
			File.Delete(dstPath);

		Directory.CreateDirectory(dstPath);

		foreach (var file in Directory.GetFiles(srcPath))
			File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

		foreach (var dir in Directory.GetDirectories(srcPath))
			CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
	}

	private static void AddFramework(string framework) {
		if (_project.HasFramework(framework)) 
			return;

		_project.AddFrameworkToProject(_target, framework, false);
	}

	private static void AddExternalFramework(string path, string framework) {
		string srcPath = path + "/" + framework;
		string dstPath = Path.Combine(_path, "Frameworks/" + framework);

		CopyAndReplaceDirectory(srcPath, dstPath);

		string frameworkGuid = _project.AddFile("Frameworks/" + framework, "Frameworks/" + framework, PBXSourceTree.Source);
		_project.AddFileToBuild(_target, frameworkGuid);
		
		AddFramework(framework);
	}

	static int GetLineIndexThatContains(List<string> lines, string content, int start = 0)
	{
		for (int i = start; i < lines.Count; ++i) {
			if (lines [i].Contains (content)) {
				return i;
			}
		}

		return -1;
	}

	static string GetNextUuid(string token)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		for (int i = 0; i < token.Length; ++i) {
			if (i == 7) {
				char c = token [i];
				if (c == '9') {
					c = 'A';
				} else if (c == 'Z' || c == 'F') {
					c = '0';
				} else {
					c = (char)(c + 1);
				}
				sb.Append (c);
			} else {
				sb.Append (token [i]);
			}
		}
		return sb.ToString ();
	}

	public static void AddFrameworkToEmbed(string path, string frameworkName)
	{
		string filePath = Path.Combine(path, "Unity-iPhone.xcodeproj/project.pbxproj");
		int tokenLength = 24;

		var lines = new List<string> (File.ReadAllLines(filePath));

		// add embed framework line
		var index = GetLineIndexThatContains(lines, frameworkName + " in Frameworks");
		var line = lines [index];
		var frameworkUuid = line.Substring(0, line.IndexOf(@"/*")).Trim ();
		var fileRef = line.Substring(line.IndexOf("fileRef = ") + "fileRef = ".Length, tokenLength);
		var embedFrameworkLineUuid = GetNextUuid(frameworkUuid);
		var embedFrameworkLine = "\t\t" + embedFrameworkLineUuid + " /* " + frameworkName + " in Embed Frameworks */ = {isa = PBXBuildFile; fileRef = " + fileRef + " /* " + frameworkName + " */; settings = {ATTRIBUTES = (CodeSignOnCopy, RemoveHeadersOnCopy, ); }; };";
		lines.Insert(index + 1, embedFrameworkLine);

		// add embed framework section if it does not exist, or insert the line
		index = GetLineIndexThatContains(lines, "/* Begin PBXCopyFilesBuildPhase section */");
		var embedFrameworkSectionLineIndex = GetLineIndexThatContains(lines, "/* Embed Frameworks */ = {", index);
		if (embedFrameworkSectionLineIndex > -1) {
			index = GetLineIndexThatContains(lines, "files = (", embedFrameworkSectionLineIndex);
			lines.Insert (++index, "\t\t\t\t" + embedFrameworkLineUuid + " /* " + frameworkName + " in Embed Frameworks */,");
		} else {
			var embedFrameworkSectionUuid = GetNextUuid(embedFrameworkLineUuid);
			lines.Insert(++index, "\t\t" + embedFrameworkSectionUuid + " /* Embed Frameworks */ = {");
			lines.Insert(++index, "\t\t\tisa = PBXCopyFilesBuildPhase;");
			lines.Insert(++index, "\t\t\tbuildActionMask = 2147483647;");
			lines.Insert(++index, "\t\t\tdstPath = \"\";");
			lines.Insert(++index, "\t\t\tdstSubfolderSpec = 10;");
			lines.Insert(++index, "\t\t\tfiles = (");
			lines.Insert(++index, "\t\t\t\t" + embedFrameworkLineUuid + " /* " + frameworkName + " in Embed Frameworks */,");
			lines.Insert(++index, "\t\t\t);");
			lines.Insert(++index, "\t\t\tname = \"Embed Frameworks\";");
			lines.Insert(++index, "\t\t\trunOnlyForDeploymentPostprocessing = 0;");
			lines.Insert(++index, "\t\t};");

			// add reference of the section
			index = GetLineIndexThatContains(lines, "/* Begin PBXNativeTarget section */");
			var indexBuildPhase = GetLineIndexThatContains(lines, "buildPhases = (", index);
			var indexBuildPhaseEnd = GetLineIndexThatContains(lines, ");", indexBuildPhase);
			lines.Insert(indexBuildPhaseEnd, "\t\t\t\t" + embedFrameworkSectionUuid + " /* Embed Frameworks */,");
		}

		File.WriteAllLines(filePath, lines.ToArray());

		string projPath = PBXProject.GetPBXProjectPath (path);
		PBXProject proj = new PBXProject();
		proj.ReadFromString(File.ReadAllText(projPath));
		string target = proj.TargetGuidByName("Unity-iPhone");
		proj.AddBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
		proj.WriteToFile (projPath);
	}

	private static void AddFile(string path, string file) {
		string srcPath = path + "/" + file;
		string dstPath = _path + "/" + file;

		File.Copy(srcPath, dstPath);
		
		string fileGuid = _project.AddFile(file, file);
    	_project.AddFileToBuild(_target, fileGuid);
	}

 	private static void AddUsrLib(string framework)
    {
        string fileGuid = _project.AddFile("usr/lib/" + framework, "Frameworks/" + framework, PBXSourceTree.Sdk);
        _project.AddFileToBuild(_target, fileGuid);
    }

	private static void UpdatePlist()
	{
		string plistPath = _path + "/Info.plist";
		PlistDocument plist = new PlistDocument();
		plist.ReadFromString(File.ReadAllText(plistPath));

		PlistElementDict rootDict = plist.root;

		rootDict.SetString("NSLocationAlwaysUsageDescription", "We use access to location always responsibly");
		rootDict.SetString("NSLocationWhenInUseUsageDescription", "We use access to location when in use responsibly");
		rootDict.SetBoolean("UIViewControllerBasedStatusBarAppearance", false);
		rootDict.CreateArray("UIBackgroundModes").AddString("remote-notification");
		rootDict.CreateDict("NSAppTransportSecurity").SetBoolean("NSAllowsArbitraryLoads", true);

		File.WriteAllText(plistPath, plist.WriteToString());
	}

	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string buildPath) {	
		if (buildTarget == BuildTarget.Android) {
			
			return;
		}

		if (buildTarget != BuildTarget.iOS) 
			return;

		_path = buildPath;
    		
		OpenProject();

		AddFramework("CoreData.framework");
		AddFramework("SafariServices.framework");
		AddFramework("AddressBook.framework");
		AddFramework("SystemConfiguration.framework");
		AddFramework("MediaToolbox.framework");

		AddUsrLib("libsqlite3.tbd");
		AddUsrLib("libz.dylib");

		AddExternalFramework(Application.dataPath + "/../NativeAssets", "VtcSDKResource.bundle");
		AddExternalFramework(Application.dataPath + "/../NativeAssets", "VtcSDK.framework");
		
		AddFile(Application.dataPath + "/../NativeAssets", "VtcSDK-Info.plist");

		_project.SetBuildProperty(_target, "IPHONEOS_DEPLOYMENT_TARGET", "9.0");
		_project.SetBuildProperty(_target, "ENABLE_BITCODE", "NO");
		_project.SetBuildProperty(_target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
		_project.AddBuildProperty(_target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks");
		_project.AddBuildProperty(_target, "OTHER_LDFLAGS", "-ObjC");
		
		UpdatePlist();

		CloseProject();

		// Please add after CloseProject method.
		AddFrameworkToEmbed(buildPath, "VtcSDK.framework");
	}
}



