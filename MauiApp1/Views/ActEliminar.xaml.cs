using MauiApp1.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Net;
using System.Text;


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

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            int codigo = int.Parse(txtCodigo.Text);

            var datosEstudiante = new Dictionary<string, string>
            {
                {"codigo", codigo.ToString()},
                {"nombre", txtNombre.Text},
                {"apellido", txtApellido.Text},
                {"edad",  txtEdad.Text}
            };

            
            var query = new FormUrlEncodedContent(datosEstudiante);
            string url = "http://192.168.200.4/moviles/post.php?" + await query.ReadAsStringAsync();

            HttpClient client = new HttpClient();

            var response = await client.PutAsync(url, null);

            // Verificar la respuesta
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Datos actualizados correctamente", "OK");
                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo actualizar los datos: {responseBody}", "OK");
            }
            

        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "OK");
        }
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