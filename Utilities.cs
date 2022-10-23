using System;

namespace ega_lab9;

public static class Utilities
{
	private static readonly Random Random = new();

	public static int GetRandom() => Random.Next();
	public static int GetRandom(int max) => Random.Next(max);
	public static int GetRandom(int min, int max) => Random.Next(min, max);
	public static double GetRandomDouble() => Random.NextDouble();

	public static double APSum(double first, double difference, int n) => (2 * first + difference * (n - 1)) / 2 * n;
}
