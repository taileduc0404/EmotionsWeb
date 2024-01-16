using CamXucWeb.Interface;
using CamXucWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static CamXucWeb.CamXucModel;

namespace CamXucWeb.Controllers
{
    public class CamXucController : Controller
	{

		private readonly IReviewService _reviewService;

        public CamXucController(IReviewService reviewService)
        {
			_reviewService = reviewService;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult PredictSentiment(CamXucVM vm)
		{
            //Load sample data
            var sampleData = new CamXucModel.ModelInput()
            {
                Col0 = vm.Text ?? string.Empty,
            };

            //Load model and predict output
            var result = CamXucModel.Predict(sampleData);


            var isPositive = result.Score[0] > result.Score[1];

            _reviewService.AddReview(vm, isPositive);

            // Pass the prediction result to the view
            var m1 = Math.Round(result.Score[0] * 100, 1);
			var m2 = Math.Round(result.Score[1] * 100, 1);

			if (m1 > m2)
			{
				ViewBag.Ketqua = "Tích cực";
			}
			else
			{
				ViewBag.Ketqua = "Tiêu cực";
			}

			return View("Index");
		}

        public async Task<IActionResult> Statistics()
        {
            var positivePercentage = await _reviewService.GetPositivePercentage();
            var negativePercentage = await _reviewService.GetNegativePercentage();

            var model = new StatisticsVM
            {
                PositivePercentage = positivePercentage,
                NegativePercentage = negativePercentage
            };

            // Make sure you are returning the correct view and passing the correct model
            return View("Statistics", model);
        }


    }
}
