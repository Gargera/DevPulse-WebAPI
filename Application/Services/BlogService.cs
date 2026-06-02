using Application.Interfaces.UnitOfWork;

namespace Application.Services
{
    public class BlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
