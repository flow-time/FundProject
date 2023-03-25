// See https://aka.ms/new-console-template for more information
using FundNotice;
using Newtonsoft.Json;

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
    var asdasd = JsonConvert.DeserializeObject<HistoryWorth>(res);
    float fixedDateNetworth = Business.GetNetWortByFixedDate(asdasd.content);
    SqlServerHelper.InsertDataToSqlServer(@"INSERT INTO dbo.FundsPurchaseInfo
    (fundId,fundName,purchaseDate,netWorth)
VALUES
    (@fundId,@fundName, @purchaseDate, @networth);",
        new { fundId = fundId, fundName ="test",purchaseDate = purchaseDate, networth = fixedDateNetworth.ToString("0.0000") });
}

Console.WriteLine($"User input string:{fundId}");
