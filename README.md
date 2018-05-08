# Deltatre.Utils

This is an utils library used inside Webplu team for all .NET projects.  
The purpose of this library is to provide developers with extension methods, DTO and types useful to solve common problems, such as checking if a sequence is null or empty or convert a dictionary to a read only dictionary.

## DTO
Following data transfer objects are available:
- NormalizationResult<T> and NormalizationResult<TValue, TError> to provide results from normalization tasks
- ValidationResult<T> and ValidationResult<TValue, TError> to provide results from validation tasks
- ParsingResult<T> and ParsingResult<TValue, TError> to provide results from parsing tasks

## DICTIONARY EXTENSIONS
Following extension methods are available for dictionary:
- AsReadOnly<TKey, TValue>: get a ReadOnlyDictionary<TKey, TValue> which wraps an instance of IDictionary<TKey, TValue>

## ENUMERABLE EXTENSIONS
Following extension methods are available for IEnumerable:
- IsNullOrEmpty<T>: checks whether an instance of IEnumerable<T> is null or empty
- ForEach<T>: invokes an instance of Action<T> for each item inside an instance of IEnumerable<T>
- HasDuplicates<T>: checks whether an instance of IEnumerable<T> contains duplicates. Optionally you can provide a comparer (an instance of IEqualityComparer<T>)
- ToNonEmptySequence<T>: converts an instance of IEnumerable<T> to an instance of NonEmptySequence<T>
