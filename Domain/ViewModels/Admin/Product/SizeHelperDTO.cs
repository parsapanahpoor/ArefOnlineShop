namespace Domain.ViewModels.Admin.Product
{
    public record SizeHelperDTO
    {
        #region properties

        public int ProductId { get; set; }

        public string SizeTitle { get; set; }

        public int GhadeLebas { get; set; }

        public int SizeDorSine { get; set; }

        public int SarShane { get; set; }

        public int GhadeAstin { get; set; }

        #endregion
    }
}
