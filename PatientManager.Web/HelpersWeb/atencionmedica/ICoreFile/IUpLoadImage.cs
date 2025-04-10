namespace PatientManager.Web.HelpersWeb.atencionmedica.ICoreFile
{
    public interface IUpLoadImage
    {
        public string UpLoadImageTo(IFormFile file, int id, bool IsEdit = false, string imageUrl = "");
    }
}
