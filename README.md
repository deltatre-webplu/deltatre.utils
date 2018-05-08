# Deltatre.Utils

This is an utils library used inside Webplu team for all .NET projects.  
The purpose of this library is to provide developers with extension methods, DTO and types useful to solve common problems, such as checking if a sequence is null or empty or convert a dictionary to a read only dictionary.

## Dto
Following data transfer objects are available:
- NormalizationResult<TValue> and NormalizationResult<TValue, TError> to provide results from normalization tasks
- ValidationResult<TValue> and ValidationResult<TValue, TError> to provide results from validation tasks
- ParsingResult<TValue> and ParsingResult<TValue, TError> to provide results from parsing tasks

## Dictionary Extension Methods
Following extension methods are available for dictionary:
- AsReadOnly<TKey, TValue>: get a ReadOnlyDictionary<TKey, TValue> which wraps an instance of IDictionary<TKey, TValue>

## IEnumerable Extension Methods
Following extension methods are available for IEnumerable:
- IsNullOrEmpty<TITem>: checks whether an instance of IEnumerable<TITem> is null or empty
- ForEach<TITem>: invokes an instance of Action<TITem> for each item inside an instance of IEnumerable<TITem>
- HasDuplicates<TITem>: checks whether an instance of IEnumerable<TITem> contains duplicates. Optionally you can provide a comparer (an instance of IEqualityComparer<TITem>)
- ToNonEmptySequence<TITem>: converts an instance of IEnumerable<TITem> to an instance of NonEmptySequence<TITem>
