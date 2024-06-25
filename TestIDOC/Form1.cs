using Infrastructure.SAPDM;

namespace TestIDOC
{
    public partial class Form1 : Form
    {
        private ApiService apiService;
        public Form1(ApiService apiService)
        {
            InitializeComponent();
            this.apiService = apiService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                var data = new { Name = "Test", Value = "123" }; // Exemplu de date de trimis

                string fileContent = File.ReadAllText(textBox1.Text);
                var response = await apiService.PostDataAsync("https://accenture-prod-opsmessolution-va627rp8.it-cpi018-rt.cfapps.eu10-003.hana.ondemand.com/cxf/GenericMessageProcessor_00", fileContent);
                var responseMessage = await response.Content.ReadAsStringAsync();
                var errorCode = response.StatusCode.ToString();
                labelResult.Text = errorCode;
                textBox2.Text = DateTime.Now.ToLongDateString() + ":\n" + responseMessage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                labelResult.Text = "Error";
            }
            button1.Enabled = true;
        }
    }
}
