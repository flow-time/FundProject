// See https://aka.ms/new-console-template for more information
using FundNotice;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

Console.Write("Input your had buied fund code(example:012414):");
string fundId = Console.ReadLine();

if (!string.IsNullOrEmpty(fundId))
{
    var res = HttpClientHelper.FundTodayWorth(fundId);
    if (string.IsNullOrEmpty(res))
        Console.WriteLine("Please input vaild fund code");

    Regex regex = new("[\\(（].*[\\)）]");
    string regexRes = regex.Match(res).Value;
    regexRes = regexRes.TrimStart('(').TrimEnd(')');

    FundTodayInfo entity = JsonSerializer.Deserialize<FundTodayInfo>(regexRes);
    //Console.Write("Input your buied date(example:2023-02-02):");
    //string buiedDate = Console.ReadLine();
    //var asdasd = DateOnly.Parse(buiedDate);
}

Console.WriteLine($"User input string:{fundId}");
