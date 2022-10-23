using System;
using System.Collections.Generic;
using System.Linq;

namespace ega_lab9;

public static class ClosestCityMethod
{
	public static (IEnumerable<int>, int) FindSolution(DistanceMatrix distances, int firstCity)
	{
		var route = new List<int> { firstCity };
		var distance = 0;

		var unvisitedCities = Enumerable.Range(0, distances.CityCount).ToList();
		unvisitedCities.Remove(firstCity);

		while (unvisitedCities.Count > 0)
		{
			Console.WriteLine($"> Итерация №{route.Count}  |  Текущий маршрут: {string.Join(" -> ", route)}");

			var (nextCity, fromIdx) = NextCity(distances, route, unvisitedCities);
			Console.WriteLine($"> Отобранный город: {nextCity}, после {route[fromIdx]}. Расстояние: {distances[route[fromIdx], nextCity]}");

			route.Insert(fromIdx+1, nextCity);
			distance = distances.RouteDistance(route);
			Console.WriteLine($"> Текущее расстояние: {distance}\n");
			unvisitedCities.Remove(nextCity);
		}

		return (route, distance);
	}

	// (city, fromIdx)
	private static (int, int) NextCity(DistanceMatrix distances, List<int> route, List<int> unvisitedCities)
	{
		// city, fromIdx
		var closestCities = new List<(int, int)>();
		var weights = new List<double>();

		for (var i = 0; i < route.Count; i++)
		{
			var from = route[i];
			var (closestCity, distance) = ClosestCity(distances, from, unvisitedCities);

			closestCities.Add((closestCity, i));
			weights.Add(1 / (double) distance);
		}

		return Roulette.Spin(closestCities, weights);
	}

	// (city, distance)
	private static (int, int) ClosestCity(DistanceMatrix distanceMatrix, int from, List<int> unvisitedCities)
	{
		var closestCity = -1;
		var closestDistance = int.MaxValue;

		foreach (var to in unvisitedCities)
		{
			var distance = distanceMatrix[from, to];
			if (distance < closestDistance)
			{
				closestCity = to;
				closestDistance = distance;
			}
		}

		return (closestCity, closestDistance);
	}
}
