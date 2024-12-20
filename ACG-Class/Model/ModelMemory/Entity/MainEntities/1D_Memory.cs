using ACG_Class.Model.ModelMemory.Abstract;

namespace ACG_Class.Model.ModelMemory.Class
{
    public class _1D_Memory : MemoryEntityBase
    {
        public int Id { get; set; }
        public string NameGroup { get; set; }
        public string Fullname { get; set; }
        public string rnokpp { get; set; }
        public string address { get; set; }
        public string edryofop_Data { get; set; }
        public string BanckAccount { get; set; }
        public string? Director { get; set; }
        public string? ResPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Status_Counterparty { get; set; }

        public DateTime added { get; set; }
        public DateTime updated { get; set; }

        public override DateTime AddedTime
        {
            get => added;
            set => added = DateTime.Now;
        }

        public override DateTime UpdatedTime
        {
            get => updated;
            set => updated = DateTime.Now;
        }
    }
}
