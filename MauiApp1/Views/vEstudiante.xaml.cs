using MauiApp1.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://192.168.200.4/moviles/post.php";//no con el localhost
	private readonly HttpClient cliente= new HttpClient();
	private ObservableCollection<Models.Estudiante> est;
	private Estudiante selectedEstudiante;


	public vEstudiante()
	{
		InitializeComponent();
		mostrar();
	}

	public async void mostrar()
	{
		var content = await cliente.GetStringAsync(Url);
		List<Models.Estudiante> mostrar = JsonConvert.DeserializeObject<List<Models.Estudiante>>(content);
		est = new ObservableCollection<Models.Estudiante>(mostrar);
		listaEstudiantes.ItemsSource = est;
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new Views.vAgregar());
    }

    private void listaEstudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedEstudiante = e.CurrentSelection.FirstOrDefault() as Estudiante;
        Navigation.PushAsync(new ActEliminar(selectedEstudiante));
    }
}