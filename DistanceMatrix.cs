using System;

namespace ega_lab9;

public sealed class DistanceMatrix<T>
{
	private readonly T[] _data;
	public int CityCount { get; }

	public DistanceMatrix(int cityCount)
	{
		CityCount = cityCount;
		_data = new T[CityCount * (CityCount - 1) / 2];
	}

	public DistanceMatrix(int cityCount, T[] data)
	{
		CityCount = cityCount;
		if (data.Length != CityCount * (CityCount - 1) / 2)
			throw new ArgumentException("Invalid data length");
		_data = data;
	}

	private int ConvertIndex(int i, int j)
	{
		if (i == j) return 0;

		if (i > j)
		{
			(i, j) = (j, i);
		}

		var idx = j - i - 1;
		var jump = Convert.ToInt32(Utilities.APSum(CityCount - 1, -1, i));

		return jump + idx;
	}

	public T this[int from, int to] => _data[ConvertIndex(from, to)];
}
