namespace LilaRent.MobileApp.Views;


public partial class LoginView : ContentPage
{
    public LoginView() 
    {
        InitializeComponent();

        //Appearing += LoginView_Appearing;
    }

    //private async void LoginView_Appearing(object? sender, EventArgs e)
    //{
    //    EnterEntry.Text = await LoadData();
    //}

    //private async Task<string> LoadData()
    //{
    //    string url = "http://192.168.43.102:7002/announcements";

    //    try
    //    {
    //        HttpClient client = new HttpClient()
    //        {
    //            Timeout = TimeSpan.FromSeconds(10)
    //        };
    //        HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
    //        response.EnsureSuccessStatusCode(); // ������� ����������, ���� ������ ������ �� �������
    //        string mes = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    //        return mes;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        // �������� � ����� ��� ��������
    //        return $"Request error: {ex.Message}";
    //    }
    //    catch (Exception ex)
    //    {
    //        // ����� ������ ������
    //        return $"Unhandled error: {ex.Message}";
    //    }
    //    finally
    //    {
    //        ;
    //    }
    //}
}
