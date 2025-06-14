using System.Windows;
using WpfApp1.DataAccess;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private PersonneRepository repo = new PersonneRepository();
        private Personne selectedPersonne;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            PersonneGrid.ItemsSource = repo.GetAll();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var p = new Personne
            {
                Nom = NomBox.Text,
                Prenom = PrenomBox.Text,
                Age = int.Parse(AgeBox.Text)
            };
            repo.Add(p);
            LoadData();
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (PersonneGrid.SelectedItem is Personne p)
            {
                p.Nom = NomBox.Text;
                p.Prenom = PrenomBox.Text;
                p.Age = int.Parse(AgeBox.Text);
                repo.Update(p);
                LoadData();
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (PersonneGrid.SelectedItem is Personne p)
            {
                repo.Delete(p.Id);
                LoadData();
            }
        }

        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {
            var results = repo.Search(SearchBox.Text);
            PersonneGrid.ItemsSource = results;
        }
    }
}
