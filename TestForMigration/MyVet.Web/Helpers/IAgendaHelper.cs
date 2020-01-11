using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
    public interface IAgendaHelper
    {
        Task AddDaysAsync(int days);
    }
}
