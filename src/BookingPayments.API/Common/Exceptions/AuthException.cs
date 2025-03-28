namespace BookingPayments.API.Common.Exceptions;

public sealed class AuthException(string message) : Exception(message) { }
