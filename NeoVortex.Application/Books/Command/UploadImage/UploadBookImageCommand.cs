using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Files;
using NeoVortex.Application.Common.Results;

namespace NeoVortex.Application.Books.Command.UploadImage;

public record UploadBookImageCommand(Guid Id, IFile Image) : IRequest<ErrorOr<AssetResult>>;