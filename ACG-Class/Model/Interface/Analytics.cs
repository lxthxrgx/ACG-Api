namespace ACG_Class.Model.Interface
{
    public interface Analytics
    {
        public int Id { get; set; }
        public int NumberGroup { get; set; }
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
        //2D

    }
}
