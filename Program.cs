using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ega_lab9.Utilities;

namespace ega_lab9;

internal static class Program
{
	private static void Main()
	{
		var (cityCount, rawDistances) = FromFile();
		var distances = new DistanceMatrix<int>(cityCount, rawDistances.ToArray());

		var (route, distance) =
			ClosestNeighbourMethod.FindSolution(distances, GetRandom(cityCount - 1));

		Console.WriteLine($"Маршрут: {string.Join(" -> ", route)}");
		Console.WriteLine($"Расстояние: {distance}");
	}

	private static (int, IEnumerable<int>) FromFile()
	{
		var lines = File.ReadLines("matrix.txt");

		var cityCount = 0;
		var rawDistances = new List<int>();
		foreach (var line in lines)
		{
			var split = line.Split(' ');
			for (var j = cityCount+1; j < split.Length; j++)
			{
				var successfulParse = int.TryParse(split[j], out var distance);
				if (!successfulParse) throw new Exception("Failed to parse distance");
				rawDistances.Add(distance);
			}

			cityCount++;
		}

		return (cityCount, rawDistances);
	}
}
