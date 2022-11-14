using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UK_UWULang;

public static partial class UWUIfier {
	const string UV = "uv";
	const string W = "w";
	const string UWU = "uwu";
	const string OWO = "owo";

	public static string UWUIfy(string text, int intensity) {
		string outp = DoRegex(text, intensity);
		return outp;
	}

	private static string DoRegex(string text, int intensity) {
		string outp = text.ToLower();
		outp = OveRegex().Replace(outp, UV);
		outp = RAndLRegex().Replace(outp, W);
		outp = VowelRegex().Replace(outp, W);
		if(intensity >= 1) outp = NoRLBeforeWRegex().Replace(outp, string.Empty);
		if(intensity >= 1) outp = NoRLAfterWRegex().Replace(outp, string.Empty);
		if(intensity >= 2) outp = IncompleteUwuRegex().Replace(outp, UWU);
		if(intensity >= 2) outp = IncompleteOwoRegex().Replace(outp, OWO);
		return outp;
	}

	[GeneratedRegex(@"(?<!<)(ove)|(of)(?![\w\d\s=]*>)")]
	private static partial Regex OveRegex();

	[GeneratedRegex(@"(?<!<)(?<![lrou])[lr](?![\w\d\s=]*>)")]
	private static partial Regex RAndLRegex();

	[GeneratedRegex(@"(?<!<)(?<!\b|[waoug])(?=[aou](?!\b|w|[aeiou]))(?![\w\d\s=]*>)")]
	private static partial Regex VowelRegex();

	[GeneratedRegex(@"(?<!<)[lr](?>w)(?![\w\d\s=]*>)")]
	private static partial Regex NoRLBeforeWRegex();

	[GeneratedRegex(@"(?<!<)(?<=w)[lr](?![\w\d\s=]*>)")]
	private static partial Regex NoRLAfterWRegex();

	[GeneratedRegex(@"(?<!<)((?<!u)wu)|(uw(?!u))(?![\w\d\s=]*>)")]
	private static partial Regex IncompleteUwuRegex();

	[GeneratedRegex(@"(?<!<)((?<!o)wo)|(ow(?!o))(?![\w\d\s=]*>)")]
	private static partial Regex IncompleteOwoRegex();
}