Class Personne
    Public Property Id As Integer
    Public Property Nom As String
    Public Property Prenom As String
    Public Property Age As Integer
End Class

Class MainWindow
    Private personnes As New List(Of Personne)
    Private selectedId As Integer = -1
    Private nextId As Integer = 1

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        RafraichirGrille()
    End Sub

    Private Sub RafraichirGrille()
        DataGrid.ItemsSource = Nothing
        DataGrid.ItemsSource = personnes
    End Sub

    Private Sub BtnAjouter_Click(sender As Object, e As RoutedEventArgs)
        Dim p As New Personne With {
            .Id = nextId,
            .Nom = txtNom.Text,
            .Prenom = txtPrenom.Text,
            .Age = CInt(txtAge.Text)
        }
        personnes.Add(p)
        nextId += 1
        RafraichirGrille()
        ViderChamps()
    End Sub

    Private Sub BtnModifier_Click(sender As Object, e As RoutedEventArgs)
        Dim p = personnes.FirstOrDefault(Function(x) x.Id = selectedId)
        If p IsNot Nothing Then
            p.Nom = txtNom.Text
            p.Prenom = txtPrenom.Text
            p.Age = CInt(txtAge.Text)
            RafraichirGrille()
            ViderChamps()
        End If
    End Sub

    Private Sub BtnSupprimer_Click(sender As Object, e As RoutedEventArgs)
        Dim p = personnes.FirstOrDefault(Function(x) x.Id = selectedId)
        If p IsNot Nothing Then
            personnes.Remove(p)
            RafraichirGrille()
            ViderChamps()
        End If
    End Sub

    Private Sub dataGrid_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim p As Personne = CType(DataGrid.SelectedItem, Personne)
        If p IsNot Nothing Then
            selectedId = p.Id
            txtNom.Text = p.Nom
            txtPrenom.Text = p.Prenom
            txtAge.Text = p.Age.ToString()
        End If
    End Sub

    Private Sub ViderChamps()
        txtNom.Text = ""
        txtPrenom.Text = ""
        txtAge.Text = ""
        selectedId = -1
    End Sub
End Class
