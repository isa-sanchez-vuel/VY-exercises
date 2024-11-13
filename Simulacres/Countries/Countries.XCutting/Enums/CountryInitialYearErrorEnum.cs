namespace Countries.XCutting.Enums
{
	public enum CountryInitialYearErrorEnum
	{
		RequestNull,
		ImporterNull,
		RepositoryNull,

		FirstLetterNotAChar,
		InvalidYear,

		ApiImportFailed,
		ApiDataImportError,

		ModelMapFailed,
		CountryListNull
	}
}
