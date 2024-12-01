using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


internal class LogoutHandler : IRequestHandler<LogoutCommand>
{
    private readonly IUnitOfWork _unitOfWork;


    public LogoutHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken = default)
    {
        Guid userId = request.UserId;

        if (await _unitOfWork.RefreshTokenRepository.FindByIdAsync(userId, cancellationToken) is null)
        {
            return;
        }

        await _unitOfWork.RefreshTokenRepository.RemoveAsync(userId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
