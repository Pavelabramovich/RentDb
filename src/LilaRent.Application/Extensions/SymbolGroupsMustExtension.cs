using FluentValidation;
using FluentValidation.Results;


namespace LilaRent.Application.Extensions;


public static class SymbolGroupsMustExtension
{
    public static IRuleBuilderOptions<T, string> SymbolGroupsMust<T>(
        this IRuleBuilder<T, string> @this, 
        Action<SymbolGroupsSettings> options)
    {
        var settings = new SymbolGroupsSettings();
        options(settings);

        return @this.Custom((@string, context) =>
        {
            HashSet<char> uppers;
            int uppersCount;

            HashSet<char> lowers;
            int lowersCount;

            HashSet<char> digits;
            int digitsCount;

            HashSet<char> spaces;
            int spacesCount;

            HashSet<char> specialSymbols;
            int specialSymbolsCount;

            SplitToGroups(
                @string,
                out uppers,
                out uppersCount,
                out lowers,
                out lowersCount,
                out digits,
                out digitsCount,
                out spaces,
                out spacesCount,
                out specialSymbols,
                out specialSymbolsCount
            );

            IEnumerable<char> letters = uppers.Concat(lowers);
            IEnumerable<char> symbols = letters.Concat(digits).Concat(spaces).Concat(specialSymbols);

            var optionsWithGroups = new[]
            {
                (settings.Symbols, symbols, @string.Length),
                (settings.Letters, letters, uppersCount + lowersCount),
                (settings.Uppers, uppers, uppersCount),
                (settings.Lowers, lowers, lowersCount),
                (settings.Digits, digits, digitsCount),
                (settings.Spaces, spaces, spacesCount),
                (settings.SpecialSymbols, specialSymbols, specialSymbolsCount),
            };

            foreach (var (options, group, count) in optionsWithGroups)
            {
                if (options.Filter is not null && group.Any(s => !options.Filter(s)))
                {
                    if (options.OptionsErrors.TryGetValue(options.Filter, out string? error))
                    {
                        context.AddFailure(context.DisplayName, AddPropertyName(error, context.DisplayName));
                    }
                    else
                    {
                        context.AddFailure(new ValidationFailure());
                    }

                    return;
                }

                if (options.MinCount is not null && count < options.MinCount)
                {
                    if (options.OptionsErrors.TryGetValue(options.MinCount, out string? error))
                    {
                        context.AddFailure(context.DisplayName, AddPropertyName(error, context.DisplayName));
                    }
                    else
                    {
                        context.AddFailure(new ValidationFailure());
                    }

                    return;
                }

                if (options.MaxCount is not null && count > options.MaxCount)
                {
                    if (options.OptionsErrors.TryGetValue(options.MaxCount, out string? error))
                    {
                        context.AddFailure(context.DisplayName, AddPropertyName(error, context.DisplayName));
                    }
                    else
                    {
                        context.AddFailure(new ValidationFailure());
                    }

                    return;
                }
            }
        })
        .Must(v => true).WithMessage("mes");
    }


    private static void SplitToGroups(
        string @string,
        out HashSet<char> uppers,
        out int uppersCount,
        out HashSet<char> lowers, 
        out int lowersCount,
        out HashSet<char> digist, 
        out int digistCount,
        out HashSet<char> spaces, 
        out int spacesCount,
        out HashSet<char> specialSymbols,
        out int specialSymbolsCount)
    {
        uppers = [];
        uppersCount = 0;

        lowers = [];
        lowersCount = 0;

        digist = [];
        digistCount = 0;

        spaces = [];
        spacesCount = 0;

        specialSymbols = [];
        specialSymbolsCount = 0;

        foreach (var symbol in @string)
        {
            if (char.IsUpper(symbol))
            {
                uppers.Add(symbol);
                uppersCount++;
            }
            else if (char.IsLower(symbol))
            {
                lowers.Add(symbol);
                lowersCount++;
            }
            else if (char.IsDigit(symbol))
            {
                digist.Add(symbol);
                digistCount++;
            }
            else if (char.IsWhiteSpace(symbol))
            {
                spaces.Add(symbol);
                spacesCount++;
            }
            else
            {
                specialSymbols.Add(symbol);
                specialSymbolsCount++;
            }
        }
    }


    private static string AddPropertyName(string errorMessage, string propertyName)
    {
        return errorMessage.Replace("{PropertyName}", propertyName);
    }

    public class SymbolGroupsSettings
    {
        private readonly SymbolGroupOptions _symbols;
        private readonly SymbolGroupOptions _letters;
        private readonly SymbolGroupOptions _uppers;
        private readonly SymbolGroupOptions _lowers;
        private readonly SymbolGroupOptions _digits;
        private readonly SymbolGroupOptions _spaces;
        private readonly SymbolGroupOptions _specialSymbols;


        public SymbolGroupsSettings()
        {
            _symbols = new SymbolGroupOptions();
            _letters = new SymbolGroupOptions();
            _uppers = new SymbolGroupOptions();
            _lowers = new SymbolGroupOptions();
            _digits = new SymbolGroupOptions();
            _spaces = new SymbolGroupOptions();
            _specialSymbols = new SymbolGroupOptions();
        }


        public ISymbolGroupOptions Symbols => _symbols;
        public ISymbolGroupOptions Letters => _letters;
        public ISymbolGroupOptions Uppers => _uppers;
        public ISymbolGroupOptions Lowers => _lowers;
        public ISymbolGroupOptions Digits => _digits;
        public ISymbolGroupOptions Spaces => _spaces;
        public ISymbolGroupOptions SpecialSymbols => _specialSymbols;


        public interface ISymbolGroupOptions
        {
            Func<char, bool>? Filter { get; }
            int? MinCount { get; }
            int? MaxCount { get; }

            public Dictionary<object, string> OptionsErrors { get; }

            ISymbolGroupErrorOptions Must(Func<char, bool> filter);
            ISymbolGroupErrorOptions MinimumCount(int minCount);
            ISymbolGroupErrorOptions MaximumCount(int maxCount);
        }

        public interface ISymbolGroupErrorOptions : ISymbolGroupOptions
        {
            ISymbolGroupOptions WithMessage(string message);
        }


        private class SymbolGroupOptions : ISymbolGroupOptions
        {
            public Func<char, bool>? Filter { get; private set; }
            public int? MinCount { get; private set; }
            public int? MaxCount { get; private set; }

            public Dictionary<object, string> OptionsErrors { get; } = [];


            public ISymbolGroupErrorOptions Must(Func<char, bool> filter)
            {
                Filter = filter;
                return new SymbolGroupErrorOptions(this, filter);
            }

            public ISymbolGroupErrorOptions MinimumCount(int minCount)
            {
                MinCount = minCount;
                return new SymbolGroupErrorOptions(this, minCount);
            }

            public ISymbolGroupErrorOptions MaximumCount(int maxCount)
            {
                MaxCount = maxCount;
                return new SymbolGroupErrorOptions(this, maxCount);
            }
        }

        private class SymbolGroupErrorOptions : ISymbolGroupErrorOptions
        {
            private readonly SymbolGroupOptions _options;
            private readonly object _lastOption;

            public Func<char, bool>? Filter => _options.Filter;
            public int? MinCount => _options.MinCount;
            public int? MaxCount => _options.MaxCount;

            public Dictionary<object, string> OptionsErrors => _options.OptionsErrors;


            public SymbolGroupErrorOptions(SymbolGroupOptions options, object lastOption)
            {
                _options = options;
                _lastOption = lastOption;
            }

            public ISymbolGroupErrorOptions Must(Func<char, bool> filter)
            {
                _options.Must(filter);
                return this;
            }

            public ISymbolGroupErrorOptions MinimumCount(int minCount)
            {
                _options.MinimumCount(minCount);
                return this;
            }

            public ISymbolGroupErrorOptions MaximumCount(int maxCount)
            {
                _options.MaximumCount(maxCount);
                return this;
            }

            public ISymbolGroupOptions WithMessage(string message)
            {
                _options.OptionsErrors[_lastOption] = message;
                return _options;
            }
        }
    }
}
