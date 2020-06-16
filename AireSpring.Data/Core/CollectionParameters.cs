
namespace AireSpring.Data.Core
{
    public enum AscDec
    {
        Asc = 0,
        Dec = 1
    }

    public class CollectionParameters
    {
        /// <summary>
        /// Parameter to order collection asc or desc
        /// </summary>
        public AscDec Ordering { get; set; }

        /// <summary>
        /// Field used to order the query from database
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// Field used for pagination, number of records for page
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Field used for pagination, number of records to skip base on the page.
        /// </summary>
        public int? Offset { get; set; }


    }
}
