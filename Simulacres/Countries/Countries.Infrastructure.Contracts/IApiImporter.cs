
namespace Countries.Infrastructure.Contracts
{
	public interface IApiImporter
	{
		Task<string> ImportData();
	}
}
