using CamXucWeb.ViewModels;

namespace CamXucWeb.Interface
{
    public interface IReviewService
    {
        void AddReview(CamXucVM vm, bool isPositive);
        Task<double> GetPositivePercentage();
        Task<double> GetNegativePercentage();
    }
}
