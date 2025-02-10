using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
	private static readonly HttpClient client = new HttpClient();

	static async Task<int> GetTeamGoalsAsync(string team, int year)
	{
		int totalGoals = 0;
		int page = 1;

		while (true)
		{
			string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
			string response = await client.GetStringAsync(url);
			JObject json = JObject.Parse(response);

			foreach (var match in json["data"])
			{
				totalGoals += int.Parse(match["team1goals"].ToString());
			}

			int totalPages = int.Parse(json["total_pages"].ToString());
			if (page >= totalPages)
				break;
			page++;
		}

		page = 1;
		while (true)
		{
			string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";
			string response = await client.GetStringAsync(url);
			JObject json = JObject.Parse(response);

			foreach (var match in json["data"])
			{
				totalGoals += int.Parse(match["team2goals"].ToString());
			}

			int totalPages = int.Parse(json["total_pages"].ToString());
			if (page >= totalPages)
				break;
			page++;
		}

		return totalGoals;
	}

	static async Task Main()
	{
		string team1 = "Paris Saint-Germain";
		int year1 = 2013;
		int goals1 = await GetTeamGoalsAsync(team1, year1);
		Console.WriteLine($"Team {team1} scored {goals1} goals in {year1}");

		string team2 = "Chelsea";
		int year2 = 2014;
		int goals2 = await GetTeamGoalsAsync(team2, year2);
		Console.WriteLine($"Team {team2} scored {goals2} goals in {year2}");
	}
}
