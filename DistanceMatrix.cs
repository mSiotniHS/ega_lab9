using System;

namespace ega_lab9;

public sealed class DistanceMatrix<T>
{
	private readonly T[] _distances;
	public int CityCount { get; }

	public DistanceMatrix(int cityCount, T[] distances)
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

	public T this[int from, int to] => _distances[ConvertIndex(from, to)];
}
