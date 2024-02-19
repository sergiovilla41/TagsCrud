namespace Simem.AppCom.Datos.Repo
{
    public class ConjuntoDatosDto
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public bool Estado { get; set; }
        public string? ConjuntoDeDatosAsociados { get; set; }

    }
}