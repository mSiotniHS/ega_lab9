using System;
using System.Collections.Generic;
using System.Linq;

namespace ega_lab9;

public static class ClosestNeighbourMethod
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

			var lastCity = route.Last();
			var nextCity = NextCity(distances, lastCity, unvisitedCities);

			Console.WriteLine($"> Отобранный город: {nextCity}, расстояние: {distances[lastCity, nextCity]}");

			route.Add(nextCity);
			distance += distances[lastCity, nextCity];

			Console.WriteLine($"> Текущее расстояние: {distance}\n");

			unvisitedCities.Remove(nextCity);
		}

		distance += distances[route.Last(), route.First()];

		return (route, distance);
	}

	private static int NextCity(DistanceMatrix distances, int currentCity, List<int> unvisitedCities)
	{
		var weights = new List<double>();
		foreach (var unvisitedCity in unvisitedCities)
		{
			weights.Add(1 / (double)distances[currentCity, unvisitedCity]);
		}

		return Roulette.Spin(unvisitedCities, weights);
	}
}
