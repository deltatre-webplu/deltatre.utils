namespace Deltatre.Utils.Functional
{
	public static partial class Functions
	{
		/// <summary>
		/// An implementation of the mathematical function f(x) = x
		/// </summary>
		/// <typeparam name="T">The type of the item passed in</typeparam>
		/// <param name="item">The item passed in</param>
		/// <returns>The same item passed in</returns>
		public static T Identity<T>(T item) => item;
	}
}
