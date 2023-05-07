// See https://aka.ms/new-console-template for more information
using FundNotice;
using Newtonsoft.Json;

Console.Write("Input your had buied fund code(example:012414):");
string fundId = Console.ReadLine();

Console.Write("Input your buied date(example:2023-02-02):");
bool flag = true;
DateTime purchaseDate = new DateTime();
while (flag)
{
    string buiedDate = Console.ReadLine();
    if (!DateTime.TryParse(buiedDate, out purchaseDate))
    {
        Console.WriteLine("Please input correct date format!(for example:2023-02-02)");
    }
    else
    {
        flag = false;
    }
}

if (purchaseDate != new DateTime())
{
    string res = HttpClientHelper.FundHistoryWorth(fundId, purchaseDate, purchaseDate);
    res = res.Replace("var apidata=", "").TrimEnd(';');
    var fundHistoryEntity = JsonConvert.DeserializeObject<FundHistoryWorth>(res);

    var fundTodayInfo = HttpClientHelper.FundTodayWorth("015042");
    fundTodayInfo = fundTodayInfo.Replace("jsonpgz(", "").Replace(");", "");
    var fundTodayEntity = JsonConvert.DeserializeObject<FundTodayEntity>(fundTodayInfo);

    float fixedDateNetworth = Business.GetNetWortByFixedDate(fundHistoryEntity.content);
    _ = SqlServerHelper.InsertDataToSqlServer(@"INSERT INTO dbo.FundsPurchaseInfo
                                            (fundId,fundName,purchaseDate,netWorth)
                                        VALUES
                                            (@fundId,@fundName, @purchaseDate, @networth);",
        new
        {
            fundId,
            fundName = fundTodayEntity.name,
            purchaseDate,
            networth = fixedDateNetworth.ToString("0.0000")
        });


}

Console.WriteLine($"User input string:{fundId}");
