namespace LogicaAplicacion.Dtos
{
	public class PaginadoResultado<T>
	{
		public IEnumerable<T> Items { get; set; } = new List<T>();
		public int TotalItems { get; set; }

		public PaginadoResultado(IEnumerable<T> items, int totalItems)
		{
			Items = items;
			TotalItems = totalItems;
		}
	}

}


