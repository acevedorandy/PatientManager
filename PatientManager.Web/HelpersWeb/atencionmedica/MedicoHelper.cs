

using PatientManager.Web.HelpersWeb.atencionmedica.ICoreFile;

namespace PatientManager.Web.HelpersWeb.atencionmedica
{
    public class MedicoHelper : IUpLoadImage
    {
        public string UpLoadImageTo(IFormFile file, int id, bool IsEdit = false, string imageUrl = "")
        {
            if (IsEdit && file == null)
            {
                return imageUrl;
            }

            string basePath = $"/images/medico/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Asegurar que el directorio existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Eliminar todas las imágenes antiguas en la carpeta antes de guardar la nueva
            if (IsEdit)
            {
                var oldImages = Directory.GetFiles(path);
                foreach (var oldImage in oldImages)
                {
                    System.IO.File.Delete(oldImage);
                }
            }

            // Generar nuevo nombre de archivo
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = $"{guid}{fileInfo.Extension}";
            string filenameWithPath = Path.Combine(path, filename);

            // Guardar la nueva imagen
            using (var stream = new FileStream(filenameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{basePath}/{filename}";
        }
    }
}
