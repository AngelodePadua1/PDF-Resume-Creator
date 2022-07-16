using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;



namespace PDF_Resume_Creator
{
    public partial class Form1 : Form
    {
        private readonly string _path = $"c:\\Users\\Angelo Miguel\\Documents\\Resume.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonFromFile;
                using (var reader = new StreamReader(_path))
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                richTextBox.Text = jsonFromFile;

                var resumeFromJson = JsonConvert.DeserializeObject<Resume>(jsonFromFile);
            }
            catch (Exception ex)
            {
                // ignored
            }

            using (SaveFileDialog sfd = new() { Filter="PDF file|*.pdf", ValidateNames=true })
            {
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document doc=new iTextSharp.text.Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();
                        doc.Add(new iTextSharp.text.Paragraph(richTextBox.Text));
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        doc.Close();
                    }
                }
            }
        }
        private Resume GetResume()
            {
                var resume = new Resume
                {
                    name = "Angelo Miguel N. de Padua",
                    label = "Software Engineer",
                    email = "angelosenpai3@gmail.com",
                    phone = 09150862209,
                    location = new Location
                    {
                    address = "Malagasang 1-B, Woodlane Subdivision, Phase B, Blk 3 Lt 12",
                    city = "Imus",
                    province = "Cavite",
                    countryCode = "Philippines",
                    postalCode = 4104
                    }
                };
                return resume;
            }

        public class Resume
        {
            public string name { get; set; }
            public string label { get; set; }
            public string email { get; set; }
            public long phone { get; set; }
            public Location location { get; internal set; }
        }

        public class Location
        {
            public string address { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string countryCode { get; set; }
            public int postalCode { get; set; }
        }
    }
}
