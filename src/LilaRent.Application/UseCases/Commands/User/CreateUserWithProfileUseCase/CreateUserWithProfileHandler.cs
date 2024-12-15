using AutoMapper;
using LilaRent.Application.Exceptions;
using LilaRent.Application.Services;
using LilaRent.Application.UseCases.Commands;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;
using Profile = LilaRent.Domain.Entities.Profile;


namespace Travels.Authentication.BusinessLogic.UseCases.Commands;


internal class CreateUserWithProfileHandler : IRequestHandler<CreateUserWithProfileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly CryptographyService _cryptographyService;


    public CreateUserWithProfileHandler(
        IUnitOfWork unitOfWork, 
        IFileService fileService,
        IMapper mapper, 
        CryptographyService cryptographyService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
        _cryptographyService = cryptographyService;
    }


    public async Task Handle(CreateUserWithProfileCommand request, CancellationToken cancellationToken = default)
    {
        var userDto = request.Dto;

        var userWithSameLogin = await _unitOfWork.UserRepository.FindByLoginAsync(userDto.Login, cancellationToken);

        if (userWithSameLogin is not null)
        {
            throw new DuplicatedIdentifierException(userDto.Login, $"User with login = {userDto.Login} already exists.");   
        }

        var salt = _cryptographyService.GenerateSalt();
        var hashedPassword = _cryptographyService.HashPassword(userDto.Password, salt);

        var image = userDto.Profile.Image;
        var imagePath = await _fileService.SaveFileAsync(image.Stream, image.Format.Extension);

        User user = _mapper.Map<User>(userDto) with
        {
            Salt = salt,
            HashedPassword = hashedPassword,
        };

        var profile = _mapper.Map<Profile>((userDto.Profile, imagePath));
        user.Profiles.Add(profile);

        try
        {
            await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await _fileService.DeleteFileAsync(imagePath);
            throw;
        }
    }
}
