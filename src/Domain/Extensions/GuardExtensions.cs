using Ardalis.GuardClauses;

namespace Domain.Extensions
{
    public static class GuardExtensions
    {
        public static string NullOrWhiteSpaceWithInputValidation(this IGuardClause guard, string input, string parameterName, Func<string, bool> predicate, string message = null)
        {
            guard.NullOrWhiteSpace(input, parameterName, message);
            return guard.InvalidInput(input, parameterName, predicate, message);
        }

        public static string OutOfLength(this IGuardClause guard, string input, string parameterName, int lengthFrom, int lengthTo, string message = null)
        {
            int inputLength = input.Length;
            message ??= $"Input length must be greater than {lengthFrom} and lesser than {lengthTo}. Input length {inputLength}";
            if (inputLength < lengthFrom || inputLength > lengthTo)
                throw new ArgumentOutOfRangeException(parameterName, message);
            return input;
        }
    }
}
