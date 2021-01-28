// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "It's fine to have hardcoded messages in English.", Scope = "module")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "It's fine to declare this static method", Scope = "member", Target = "~M:Deltatre.Utils.Dto.ValidationResult`1.Invalid(System.Collections.Generic.IEnumerable{`0})~Deltatre.Utils.Dto.ValidationResult`1")]
