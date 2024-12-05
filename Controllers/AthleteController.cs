using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CS330_Fall2024_FinalProject.Core.Repositories;


namespace CS330_Fall2024_FinalProject.Controllers
{
    public class AthleteController : Controller{
        
        private readonly IUnitOfWork _unitOfWork;
        public AthleteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(){
            var athletes = _unitOfWork.Athlete.GetUser();
            return View(athletes);
        }

        public IActionResult Edit(int id){
            var athlete = _unitOfWork.Athlete.GetUser(id);
            return View(athlete);
        }

        
    }
}