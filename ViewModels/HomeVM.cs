using CamXucWeb.Models;
using X.PagedList;

namespace CamXucWeb.ViewModels
{
    public class HomeVM
    {
        public IPagedList<Review>? Reviews { get; set; }
    }
}
