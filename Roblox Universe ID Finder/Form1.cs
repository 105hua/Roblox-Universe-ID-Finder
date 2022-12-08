using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Roblox_Universe_ID_Finder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) // Runs when the user clicks the find button.
        {

            string input = InputBox.Text; // Get input from input box.

            if(input == null || input == string.Empty) // If input is null or empty.
            { // Opening of if statement.

                MessageBox.Show("Please enter a place ID!", "Error"); // Show message indicating that valid input is required.
                return; // Return.

            } // Closing of if statement.

            HttpClient client = new HttpClient();

            try
            {

                string requestURL = "https://apis.roblox.com/universes/v1/places/" + input + "/universe"; // Build request url with input.

                HttpResponseMessage response = await client.GetAsync(requestURL); // Get built request url.

                response.EnsureSuccessStatusCode(); // Ensures success status code so response is guaranteed beyond this point.

                string responseContent = await response.Content.ReadAsStringAsync();

                var responseJSON = JsonConvert.DeserializeObject<ResponseJSON>(responseContent);

                if(responseJSON == null)
                {

                    throw new Exception("Response JSON is null.");

                }
                else
                {

                    Clipboard.SetText(responseJSON.universeID.ToString());
                    MessageBox.Show("Success! Your Universe ID is " + responseJSON.universeID + ", it's been copied to your clipboard.");

                }

            }catch(Exception err)
            {

                MessageBox.Show($"An error occurred while trying to find your Universe ID. {err.Message}");

            }

        }
    }
}