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
		var (cityCount, distances) = FromFile();
		var matrix = new DistanceMatrix<int>(cityCount, distances.ToArray());

		var (path, distance) =
			ClosestNeighbourMethod.FindSolution(matrix, GetRandom(cityCount - 1));

		Console.WriteLine($"Маршрут: {string.Join(" -> ", path)}");
		Console.WriteLine($"Расстояние: {distance}");
	}

	private static (int, IEnumerable<int>) FromFile()
	{
		var lines = File.ReadLines("matrix.txt");

		var i = 0;
		var distances = new List<int>();
		foreach (var line in lines)
		{
			var split = line.Split(' ');
			for (var j = i+1; j < split.Length; j++)
			{
				var successfulParse = int.TryParse(split[j], out var distance);
				if (!successfulParse) throw new Exception("Failed to parse distance");
				distances.Add(distance);
			}

			i++;
		}

		return (i, distances);
	}
}
