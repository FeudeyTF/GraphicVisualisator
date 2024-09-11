namespace GraphicVisualisator
{
    static class Program
    {
        public static MainPage? MainPage;

        static Program()
        {
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainPage = new();
            Application.Run(MainPage);
        }
    }
}