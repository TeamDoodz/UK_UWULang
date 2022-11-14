using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace UK_UWULang;

internal class Program {
	static readonly string[] localeCategories = new string[] {
		"credits",
		"style",
		"cyberGrind",
		"books",
		"intermission",
		"frontend",
		"visualnovel",
		"options",
		"pauseMenu",
		"levelNames",
		"levelChallenges",
		"levelTips",
		"shop",
		"enemyNames",
		"cheats",
		"enemybios",
		"tutorial",
		"prelude",
		"act1",
		"act2",
		"primeSanctum",
		"secretLevels",
		"subtitles",
		"misc"
	};

	static void Main(string[] args) {
		var serializer = new JsonSerializer();
		serializer.Formatting = Formatting.Indented;

		CreateLang(serializer, 0);
		CreateLang(serializer, 1);
		CreateLang(serializer, 2);
	}

	private static void CreateLang(JsonSerializer serializer, int intensity) {
		TextReader templateReader = new StreamReader(File.OpenRead("template.json"));
		JObject lang = serializer.Deserialize<JObject>(new JsonTextReader(templateReader)) ?? throw new Exception("Failed to deserialize template.");

		try {
			lang["metadata"]["langName"] = $"uwulang_{intensity}";
			lang["metadata"]["langAuthor"] = "TeamDoodz";
			lang["metadata"]["langDisplayName"] = $"UwULang {intensity+1}";
			lang["body"]["bodyName"] = UWUIfier.UWUIfy("Converts all of Ultrakill's strings into UWUspeak.", intensity);
		} catch(Exception ex) {
			Console.WriteLine($"There was an error writing language metadata: {ex}");
			return;
		}

		foreach(string key in localeCategories) {
			if(!lang.ContainsKey(key)) continue;
			JToken? child = lang[key];
			if(child is not JObject obj) continue;
			ConvertAll(obj, intensity);
		}

		using(StreamWriter sw = File.CreateText($"uwulang_{intensity}.json")) {
			serializer.Serialize(new JsonTextWriter(sw), lang);
		}
	}

	static void ConvertAll(JObject obj, int intensity) {
		List<KeyValuePair<string, string>> toModify = new();
		foreach(var pair in obj) {
			toModify.Add(new(pair.Key, UWUIfier.UWUIfy(pair.Value?.ToString() ?? "null", intensity)));
		}
		foreach(var pair in toModify) {
			obj[pair.Key] = pair.Value;
		}
	}
}
