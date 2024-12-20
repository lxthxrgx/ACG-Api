using ACG_Class.Model.ModelMemory.Abstract;

namespace ACG_Class.Model.ModelMemory.Class
{
    public class _5D_Memory : MemoryEntityBase
    {
        public int Id { get; set; }
        public int NumberGroup { get; set; }
        public string NameGroup { get; set; }
        public string address { get; set; }
        public string OhronnaComp { get; set; }
        public string NumDog { get; set; }
        public string? NumDog2 { get; set; }
        public DateTime StrokDii { get; set; }
        public DateTime? StrokDii2 { get; set; }
        public string? ResPerson { get; set; }
        public string? Phone { get; set; }

        public ICollection<PathToFilesGuard> PathToFilesGuard { get; } = new List<PathToFilesGuard>();

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

    public class PathToFilesGuard
    {
        public int Id { get; set; }

        public int _5dId { get; set; }
        public _5D_Memory D5class { get; set; }
        public string? PathTOServerFiles { get; set; }
    }

}
