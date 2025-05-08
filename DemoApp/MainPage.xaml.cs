
namespace DemoApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void HybridWebView_WebResourceRequested(object sender, HybridWebViewWebResourceRequestedEventArgs e)
	{
		// NOTES:
		// * This method MUST be synchronous, as it is called from the WebView's thread.
		// * This method MUST return a response (even if it is not yet complete), otherwise the 
		//   WebView may freeze or return a error response.
		// * The response must be set using the SetResponse method of the event args.

		// Only handle requests for the specific image URL
		if (!e.Uri.ToString().Contains("sample-image.png"))
			return;

		// Prevent the default behavior of the web view
		e.Handled = true;

		// Return the stream or task of stream that contains the content
		// NOTE: the method is NOT awaited, the WebView will continue to load the content
		e.SetResponse(200, "OK", "image/png", GetStreamAsync());
    }

	private async Task<Stream?> GetStreamAsync()
	{
		await Task.Delay(5000); // Simulate a delay

		var stream = await FileSystem.OpenAppPackageFileAsync("dotnet_bot.png");

		return stream;
	}
}
