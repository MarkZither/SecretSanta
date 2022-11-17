using System;

using Windows.UI.Xaml;

namespace SecretSanta.Uno.Wasm
{
    /// <summary>
    /// Entry point to the Uno wasm app.
    /// </summary>
    public class Program
    {
        private static App _app;

        static int Main(string[] args)
        {
            Windows.UI.Xaml.Application.Start(_ => _app = new App());

            return 0;
        }
    }
}
