namespace Maray.Helpers
{
    internal class DownloadHelper
    {
        /// <summary> DownloadString </summary>
        /// <param name="url"> </param>
        public async Task<string> DownloadStringAsync(string url, bool blProxy, string userAgent)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(1000 * 30);
                var result = await HttpClientHelper.GetInstance().GetAsync(url, cts.Token);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
            
        }
    }
}