using System;
using System.Collections.Generic;
using System.Linq;

namespace ega_lab9;

public static class Roulette
{
	public static T Spin<T>(List<T> items, List<double> weights)
	{
		if (items.Count != weights.Count)
		{
			throw new ArgumentException("Items and weights must have the same length");
		}

		var totalWeight = weights.Sum();

		var randomNum = Utilities.GetRandomDouble() * totalWeight;
		var currentSector = 0.0;

		for (var i = 0; i < items.Count; i++)
		{
			currentSector += weights[i];
			if (randomNum <= currentSector)
			{
				return items[i];
			}
		}

		throw new Exception("That can't be right");
	}
}
