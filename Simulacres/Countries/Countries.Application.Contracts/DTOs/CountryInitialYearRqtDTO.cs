﻿using Countries.XCutting.GlobalVariables;
using System.ComponentModel.DataAnnotations;

namespace Countries.Application.Contracts.DTOs
{
	public class CountryInitialYearRqtDTO
	{
		public string CountryFirstLetter { get; set; }

		public string Year { get; set; }
	}
}
