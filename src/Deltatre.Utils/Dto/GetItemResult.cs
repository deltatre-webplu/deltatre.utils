namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// This class represents the result for the operation of fetching an item from a source
	/// </summary>
	/// <typeparam name="T">The type of item being fetched</typeparam>
	public class GetItemResult<T>
	{
		/// <summary>
		/// Use this method to create an instance representing a successful get operation (a get operation in which the item was found).
		/// </summary>
		/// <param name="item">The fetched item</param>
		/// <returns>An instance representing a get operation in which the item was found</returns>
		public static GetItemResult<T> CreateForItemFound(T item) => 
			new GetItemResult<T>(true, item);

		/// <summary>
		/// Use this method to create an instance representing a failed get operation (a get operation in which the item was not found).
		/// </summary>
		/// <returns>An instance representing a get operation in which the item was not found</returns>
		public static GetItemResult<T> CreateForItemNotFound() =>
			new GetItemResult<T>(false, default(T));

		/// <summary>
		/// Indicates whether the item has been found
		/// </summary>
		public bool Found { get; }

		/// <summary>
		/// This is the fetched item. If the item was not found this property will be set equal to the default value of type T
		/// </summary>
		public T Item { get; }

		private GetItemResult(bool found, T item)
		{
			Found = found;
			Item = item;
		}
	}
}
