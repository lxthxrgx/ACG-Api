using ACG_Class.Model.ModelMemory.Abstract;

namespace ACG_Class.Model.ModelMemory.Class
{
    public class _4D_Memory : MemoryEntityBase
    {
        public int Id { get; set; }
        public int NumberGroup { get; set; }
        public string NameGroup { get; set; }
        public string address { get; set; }
        public string DogovirSuborendu { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime EndAktDate { get; set; }
        public string Suma { get; set; }
        public string? Suma2 { get; set; }
        public DateTime AktDate { get; set; }

        public ICollection<PdfFilePath_Sublease_Memory> PathToPdfFiles_Sublease_Memory { get; } = new List<PdfFilePath_Sublease_Memory>();

        public bool? Done { get; } = false;

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

    public class PdfFilePath_Sublease_Memory
    {
        public int Id { get; set; }
        public int _4DId { get; set; }
        public _4D_Memory _4D { get; set; }
        public string? PathToPdfFile_Sublease { get; set; }

    }

}