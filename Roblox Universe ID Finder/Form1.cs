// Imports.
using Newtonsoft.Json;

namespace Roblox_Universe_ID_Finder // Namespace.
{ // Opening of namespace.
    public partial class Form1 : Form // The partial class form1 will inherit form to make a window.
    { // Opening of partial class.
        public Form1() // Form1s main constructor.
        { // Opening of constructor.
            InitializeComponent(); // Call initializecomponent, sets up window and controls.
        } // Closing of constructor.

        private async void button1_Click(object sender, EventArgs e) // Runs when the user clicks the find button.
        { // Opening of void.

            string input = InputBox.Text; // Get input from input box.

            if(input == null || input == string.Empty) // If input is null or empty.
            { // Opening of if statement.

                MessageBox.Show("Please enter a place ID!", "Error"); // Show message indicating that valid input is required.
                return; // Return.

            } // Closing of if statement.

            HttpClient client = new HttpClient(); // Create new http client to interact with roblox api.

            try // Enter try to handle exceptions.
            { // Opening of try.

                string requestURL = "https://apis.roblox.com/universes/v1/places/" + input + "/universe"; // Build request url with input.

                HttpResponseMessage response = await client.GetAsync(requestURL); // Get built request url.

                response.EnsureSuccessStatusCode(); // Ensures success status code so response is guaranteed beyond this point.

                string responseContent = await response.Content.ReadAsStringAsync(); // Read response content as string.

                var responseJSON = JsonConvert.DeserializeObject<ResponseJSON>(responseContent); // Deserialize response content with responsejson class.

                if(responseJSON == null) // If deserialized response is null.
                { // Opening of if statement.

                    throw new Exception("Response JSON is null."); // Throw a new exception indicating that response json is null.

                } // Closing of if statement.
                else // In any other case.
                { // Opening of else statement.

                    Clipboard.SetText(responseJSON.universeID.ToString()); // Set clipboard text to the universe id converted to string.
                    MessageBox.Show("Success! Your Universe ID is " + responseJSON.universeID + ", it's been copied to your clipboard."); // Show message that universe id was found and its been copied to clipboard.

                } // Closing of else statement.

            } // Closing of try.
            catch(Exception err) // If error, catch exception.
            { // Opening of catch.

                MessageBox.Show($"An error occurred while trying to find your Universe ID. {err.Message}"); // Show message that error occurred and provide error.

            } // Closing of catch.

        } // Closing of void.
    } // Closing of partial class.
} // Closing of namespace.