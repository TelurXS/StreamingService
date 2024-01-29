﻿using Domain.Entities;
using System.ComponentModel;
using System.Reflection;

namespace Domain.Extensions;

public static class EnumExtensions
{
	public static string GetDescription(this Enum value)
	{
		var attribute = value.GetType()
			.GetField(value.ToString())?
			.GetCustomAttributes(typeof(DescriptionAttribute), false)
			.SingleOrDefault() as DescriptionAttribute;

		return attribute?.Description ?? value.ToString();
	}
}
