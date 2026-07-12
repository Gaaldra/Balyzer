namespace Balyzer.Application.Cards.Common;

public sealed record CardResponse(Guid Id, string HolderName, string LastDigits);