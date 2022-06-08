namespace VirtualPLC
{
    /// <summary>
    /// Retain değerleri saklamak için kullanılır.
    /// </summary>
    public class RetainSerializationData
    {
        /// <summary>
        /// Değere sahip property'nin bulunduğu kapsam içerisindeki tam adresi.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Değer
        /// </summary>
        public object Value { get; set; }
    }
}
