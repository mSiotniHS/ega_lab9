using System;
using System.Collections.Generic;
using System.Linq;

namespace ega_lab9;

public sealed class DistanceMatrix
{
	private readonly int[] _distances;
	public int CityCount { get; }

	public DistanceMatrix(int cityCount, int[] distances)
	{
		CityCount = cityCount;
		if (distances.Length != CityCount * (CityCount - 1) / 2)
			throw new ArgumentException("Invalid data length");
		_distances = distances;
	}

	private int ConvertIndex(int from, int to)
	{
		if (from == to) return 0;

		if (from > to)
		{
			(from, to) = (to, from);
		}

		var idx = to - from - 1;
		var jump = Convert.ToInt32(Utilities.APSum(CityCount - 1, -1, from));

		return jump + idx;
	}

	public int this[int from, int to] => _distances[ConvertIndex(from, to)];

	public int RouteDistance(List<int> route)
	{
		var distance = 0;
		for (var i = 0; i < route.Count - 1; i++)
		{
			distance += this[route[i], route[i + 1]];
		}

		distance += this[route.Last(), route[0]];

		return distance;
	}
}
