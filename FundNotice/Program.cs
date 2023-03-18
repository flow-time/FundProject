// See https://aka.ms/new-console-template for more information
using FundNotice;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

Console.WriteLine("Hello, World!");

Console.Write("Input your had buied fund code(example:012414):");
string fundId = Console.ReadLine();

//if (!string.IsNullOrEmpty(fundId))
//{
//    var res = HttpClientHelper.FundTodayWorth(fundId);
//    if (string.IsNullOrEmpty(res))
//        Console.WriteLine("Please input vaild fund code");

//    Regex regex = new("[\\(（].*[\\)）]");
//    string regexRes = regex.Match(res).Value;
//    regexRes = regexRes.TrimStart('(').TrimEnd(')');

//    FundTodayInfo entity = JsonSerializer.Deserialize<FundTodayInfo>(regexRes);

//    Business.InsertFundInfoToDatabase(entity);

//}

Console.Write("Input your buied date(example:2023-02-02):");
bool flag = true;
DateTime purchaseDate = new DateTime();
while (flag)
{
    string buiedDate = Console.ReadLine();
    if (!DateTime.TryParse(buiedDate, out purchaseDate))
    {
        Console.WriteLine("Please input correct date format!(for example:2023-2-2)");
    }
    else
    {
        flag = false;
    }
}

if (purchaseDate != null)
{
    string res = HttpClientHelper.FundHistoryWorth(fundId, purchaseDate, purchaseDate);
    res = res.Replace("var apidata=", "").TrimEnd(';');
    var sss = JsonSerializer.Deserialize<HistoryWorth>(res);
}

Console.WriteLine($"User input string:{fundId}");
