using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp4
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            SeismicConditionsN = SeismicConditionN.GetSeismicConditionsN(1);
        }

        private ObservableCollection<SeismicConditionN> _seismicConditionsN;
        public ObservableCollection<SeismicConditionN> SeismicConditionsN
        {
            get { return _seismicConditionsN; }
            set { if (value != _seismicConditionsN) _seismicConditionsN = value; RaisePropertyChanged("SeismicConditionsN"); }
        }

        private int _defaultIndex;

        public int DefaultIndex
        {
            get { return _defaultIndex; }
            set { _defaultIndex = value; RaisePropertyChanged(nameof(DefaultIndex)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SeismicConditionN : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        private int _imageID;

        public int ImageID
        {
            get { return _imageID; }
            set { _imageID = value; RaisePropertyChanged(nameof(ImageID)); }
        }


        private byte[] _image;
        [XmlIgnore]
        public byte[] Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (value != _image) _image = value;
                RaisePropertyChanged("Image");
            }
        }
        public static ObservableCollection<SeismicConditionN> GetSeismicConditionsN(int selectedDesignMethod)
        {
            ObservableCollection<SeismicConditionN> seismicConditionsN = new ObservableCollection<SeismicConditionN>();
            try
            {
                byte[] image = LoadImage();
                for (int i = 1; i < 6; i++)
                {
                    seismicConditionsN.Add(new SeismicConditionN { Name = "A" + i, Value = i,Image = image });
                }
            }
            catch (Exception ex)
            {
            }
            return seismicConditionsN;
        }

        public static byte[] LoadImage()
        {
            string path = string.Empty;
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Locati‌​on);

            byte[] bytes;
            Image myImg = System.Drawing.Image.FromFile(path + "\\Images\\Placeholder.png");
            using (MemoryStream ms = new MemoryStream())
            {
                myImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        #region INotifyPropertyChanged
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
