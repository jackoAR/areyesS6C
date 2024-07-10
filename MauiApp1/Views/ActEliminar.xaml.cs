using MauiApp1.Models;
using System.Net;

namespace MauiApp1.Views;

public partial class ActEliminar : ContentPage
{
	public ActEliminar(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
	}

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();

            var idCod = txtCodigo.Text;
            int codigo = int.Parse(idCod);
           

            cliente.UploadValues("http://192.168.200.4/moviles/post.php?codigo="+codigo, "DELETE", parametros);
            Navigation.PushAsync(new vEstudiante());

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "OK");
        }
    }
}