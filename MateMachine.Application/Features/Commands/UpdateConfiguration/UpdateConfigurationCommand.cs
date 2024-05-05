using MediatR;

namespace MateMachine.Application.Features.Commands.UpdateConfiguration;

public record UpdateConfigurationCommand(IEnumerable<Tuple<string, string, double>> ConversionRates) : IRequest;